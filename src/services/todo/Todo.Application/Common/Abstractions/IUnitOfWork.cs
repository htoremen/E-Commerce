using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Abstractions.Repositories;

namespace Todo.Application.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IParameterRepository Parameter { get; }
        void Commit();
        void Rollback();
        // IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}