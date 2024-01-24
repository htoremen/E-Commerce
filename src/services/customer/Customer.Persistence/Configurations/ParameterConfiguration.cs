using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistence.Configurations
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}
