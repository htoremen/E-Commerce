using System.Linq.Expressions;

namespace Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext Context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        Context = context;
        _dbSet = Context.Set<TEntity>();
    }

    public DbSet<TEntity> Entities() => _dbSet;    

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    public TEntity Get(int id)
    {
        return _dbSet.Find(id);
    }
    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }
    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
