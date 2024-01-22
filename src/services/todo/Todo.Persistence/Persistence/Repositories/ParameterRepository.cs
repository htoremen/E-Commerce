namespace Todo.Persistence.Todo.Persistence.Repositories
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        public ParameterRepository(DbContext context) : base(context)
        {
        }
    }
}