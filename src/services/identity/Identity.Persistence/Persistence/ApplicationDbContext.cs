using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Persistence.Identity.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }


        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<LoginHistory> LoginHistories { get; set; } = null!;
        public virtual DbSet<UserSocialAccount> UserSocialAccounts { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<UserForgotPassword> UserForgotPasswords { get; set; } = null!;
        public virtual DbSet<UserRegisterStage> UserRegisterStages { get; set; }

        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParameterType> ParameterTypes { get; set; }
        public virtual DbSet<ParameterStage> ParameterStages { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<User>()
                .HasOne(a => a.UserSocialAccount)
                .WithOne(b => b.User)
                .HasForeignKey<UserSocialAccount>(b => b.Id);

            builder.Entity<User>()
                .HasOne(a => a.UserForgotPassword)
                .WithOne(b => b.User)
                .HasForeignKey<UserForgotPassword>(b => b.Id);

            base.OnModelCreating(builder);
        }
    }
}
