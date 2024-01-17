using Application.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IParameterRepository Parameter {  get; }
    void Commit();
    void Rollback();
   // IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}
