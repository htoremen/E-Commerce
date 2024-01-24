namespace Customer.Persistence.Repositories
{
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
