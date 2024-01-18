using Microsoft.EntityFrameworkCore;
using MassTransit.EntityFrameworkCoreIntegration;

namespace Saga.Persistence;

public class StateDbContext : SagaDbContext
{
    public StateDbContext(DbContextOptions<StateDbContext> options) : base(options) { }

    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new StateMap();
        }
    }
}
