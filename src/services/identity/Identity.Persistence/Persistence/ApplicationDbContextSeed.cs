namespace Identity.Persistence.Identity.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static void Migrate(ApplicationDbContext _context)
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            ParameterSeedAsync(_context).Wait();
        }

        private static async Task ParameterSeedAsync(ApplicationDbContext _context)
        {
            if (!_context.Parameters.Any())
            {
                _context.ParameterTypes.AddRange(GetParameterTypes());
                await _context.SaveChangesAsync();
            }
        }

        private static IEnumerable<ParameterType> GetParameterTypes()
        {
            var list = new List<ParameterType>
            {
                new ParameterType
                {
                    Id = "4d4b3802-2128-44c8-ad98-47fe3000c100",
                    Name = "Email İzni",
                    Parameters = new List<Parameter>
                    {
                        new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c101", ParameterName = "Randevu Oluşturma Bildirimi", IsActive = true },
                        new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c102", ParameterName = "Randevu Hatırlatma Bildirimi", IsActive = true },
                        new Parameter { Id = "4d4b3802-2128-44c8-ad98-47fe3000c103", ParameterName = "Randevu Tarihi Güncelleme Bildirimi", IsActive = true }
                    }
                }
            };

            return list;
        }
    }
}
