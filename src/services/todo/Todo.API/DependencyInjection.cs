using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Todo.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInWebApi(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
            //services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

            services.AddExceptionHandler<CustomExceptionHandler>();

            AddInStaticValues(appSettings);
            AddInSwagger(services);
            AddInJwtAuthentication(services);

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddQueueConfiguration(out IQueueConfiguration queueConfiguration);
            var messageBroker = appSettings.MessageBroker;

            services.AddMassTransit(x => { UsingRabbitMq(x, messageBroker, queueConfiguration); });
            services.ConfigureMassTransitHostOptions(messageBroker);

            return services;
        }
        private static void UsingRabbitMq(IBusRegistrationConfigurator x, MessageBrokerOptions messageBroker, IQueueConfiguration queueConfiguration)
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetSnakeCaseEndpointNameFormatter();

            x.AddConsumer<CreateTodoConsumer, CreateTodoConsumerDefinition>();

            var config = messageBroker.RabbitMQ;
            x.UsingRabbitMq((context, cfg) =>
            {
                var mediator = context.GetRequiredService<IMediator>();
                cfg.Host(config.HostName, config.VirtualHost, h =>
                {
                    h.Username(config.UserName);
                    h.Password(config.Password);
                });
                cfg.UseJsonSerializer();

                cfg.ReceiveEndpoint(queueConfiguration.Names[QueueName.CreateTodo], e => { e.ConfigureConsumer<CreateTodoConsumer>(context); });

                cfg.ConfigureEndpoints(context);
            });
        }

        public static IServiceCollection ConfigureMassTransitHostOptions(this IServiceCollection services, MessageBrokerOptions messageBroker)
        {
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromMinutes(5);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(messageBroker.RabbitMQ.HostName, messageBroker.RabbitMQ.VirtualHost, h =>
                {
                    h.Username(messageBroker.RabbitMQ.UserName);
                    h.Password(messageBroker.RabbitMQ.Password);
                });
            });

            services.AddSingleton<IPublishEndpoint>(bus);
            services.AddSingleton<ISendEndpointProvider>(bus);
            services.AddSingleton<IBus>(bus);
            services.AddSingleton<IBusControl>(bus);

            return services;
        }
        private static void AddInStaticValues(AppSettings appSettings)
        {
            StaticValues.Secret = appSettings.Authenticate.Secret;
        }

        private static void AddInSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
      });
            });

        }

        private static void AddInJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, // Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanacağını belirlediğimiz alandır.
                    ValidateIssuer = false, // Oluşturulacak token değerini kimin dağıttığını ifade edeceğimiz alandır.
                    ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                    ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulamasıdır.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(StaticValues.Secret)),
                    ClockSkew = TimeSpan.Zero // Üretilecek token değerinin expire süresinin belirtildiği değer kadar uzatılmasını sağlayan özelliktir. 
                };
            });
        }
    }
}