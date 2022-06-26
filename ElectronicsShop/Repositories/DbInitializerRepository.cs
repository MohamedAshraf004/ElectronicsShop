using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicsShop.Data;
using ElectronicsShop.IRepos;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.Repositories
{
    public class DbInitializerRepository : IDbInitializerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializerRepository> _logger;

        public DbInitializerRepository(ApplicationDbContext context,ILogger<DbInitializerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Initialize()
        {
            try
            {
                if ((_context.Database.GetPendingMigrationsAsync().Result)?.Count() > 0)
                    _context.Database.Migrate();

                //TODO: Seeding Data
            }
            catch (Exception e)
            {
                _logger.LogError("Couldn't migrate database.",e);
                throw;
            }
        }
    }
}
