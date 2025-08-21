using Microsoft.EntityFrameworkCore;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Infrastructure.Data;

namespace STS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly STSDbContext _context;

        public ProductRepository(STSDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Include(p => p.Stocks)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                                 .Include(p => p.Stocks)
                                 .ToListAsync();
        }
        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

    }
}
