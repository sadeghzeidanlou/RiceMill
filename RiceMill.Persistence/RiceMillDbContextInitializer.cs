using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RiceMill.Persistence
{
    internal class RiceMillDbContextInitializer
    {
        private readonly ILogger<RiceMillDbContextInitializer> _logger;
        private readonly RiceMillDbContext _context;

        public RiceMillDbContextInitializer(ILogger<RiceMillDbContextInitializer> logger, RiceMillDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }

        //public async Task SeedAsync()
        //{
        //    try
        //    {
        //        await TrySeedAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while seeding the database.");
        //        throw;
        //    }
        //}

        //public async Task TrySeedAsync()
        //{
        //    await Task.CompletedTask;
        //}
    }
}