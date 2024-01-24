using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Common.Abstractions
{
    public interface IApplicationDbContext
    {

        DbSet<Domain.Entities.User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<LoginHistory> LoginHistories { get; }
        DbSet<UserSocialAccount> UserSocialAccounts { get; }
        DbSet<UserNotification> UserNotifications { get; }
        DbSet<UserForgotPassword> UserForgotPasswords { get; }
        DbSet<UserRegisterStage> UserRegisterStages { get; }
        DbSet<ParameterStage> ParameterStages { get; }
        DbSet<Parameter> Parameters { get; }
        DbSet<ParameterType> ParameterTypes { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
