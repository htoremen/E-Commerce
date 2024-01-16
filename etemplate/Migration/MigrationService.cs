
using Migration.App.Seed;
using Persistence.Persistence;

namespace Migration;

public interface IMigrationService
{
    void Migrate();
}

public class MigrationService : IMigrationService
{
    private readonly ApplicationDbContext _context;

    public MigrationService(ApplicationDbContext context)
    {
        this._context = context;
    }

    public void Migrate()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        ParamaterSeed.SeedAsync(_context).Wait();
    }
}