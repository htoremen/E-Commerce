using Microsoft.EntityFrameworkCore;

namespace Customer.Application.Common.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Parameter> Parameters { get; }
        DbSet<ParameterType> ParameterTypes { get; }

        DbSet<Domain.Entities.Customer> Customers { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
