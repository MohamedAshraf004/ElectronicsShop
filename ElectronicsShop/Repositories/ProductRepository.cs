using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicsShop.Data;
using ElectronicsShop.IRepos;
using ElectronicsShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectronicsShop.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context,
            ILogger<ProductRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }


    }
}
