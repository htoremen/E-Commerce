using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<ParameterType> ParameterTypes { get; set; }
}
