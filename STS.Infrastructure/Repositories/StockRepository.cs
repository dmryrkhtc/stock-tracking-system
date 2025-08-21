using STS.Domain.Entities;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STS.Application.IRepositories;

namespace STS.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly STSDbContext _context;

        public StockRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks
                                 .Include(s => s.Product)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _context.Stocks
                                 .Include(s => s.Product)
                                 .ToListAsync();
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public void Update(Stock stock)
        {
            _context.Stocks.Update(stock);
            _context.SaveChanges();
        }

        public void Delete(Stock stock)
        {
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
        }
    }
}
