using Application.Abstractions;
using Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Persistence;
using Persistence.Persistence.Repositories;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IParameterRepository Parameter { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        Parameter = new ParameterRepository(context);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Rollback()
    {
        // Rollback changes if needed
    }

    //public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    //{
    //    if (_repositories.ContainsKey(typeof(TEntity)))
    //    {
    //        return (IRepository<TEntity>)_repositories[typeof(TEntity)];
    //    }

    //    var repository = new Repository<TEntity>(_context);
    //    _repositories.Add(typeof(TEntity), repository);
    //    return repository;
    //}

    public void Dispose()
    {
        _context.Dispose();
    }
}
