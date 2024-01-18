using MassTransit;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Saga.Persistence;

public class StateMap : SagaClassMap<StateInstance>
{
    protected override void Configure(EntityTypeBuilder<StateInstance> entity, ModelBuilder model)
    {
        entity.Property(p => p.CurrentState);
    }
}
