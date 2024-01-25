using Core.Events.TodoItems;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Common.Behaviours;

namespace Todo.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInApplication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            });

            AddInMessageBusSender(services, appSettings);
            return services;
        }

        private static IServiceCollection AddInMessageBusSender(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddMessageBusSender<ICreateTodo>(appSettings.MessageBroker);
            services.AddMessageBusSender<IAddTodoItem>(appSettings.MessageBroker);
            services.AddMessageBusSender<IDeleteTodo>(appSettings.MessageBroker);
            services.AddMessageBusSender<ICompleteTodo>(appSettings.MessageBroker);

            return services;
        }
    }
}