using Application.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.Persistence.Repositories;

public class ParameterRepository : Repository<Parameter>, IParameterRepository
{
    public ParameterRepository(DbContext context) : base(context)
    {
    }
}
