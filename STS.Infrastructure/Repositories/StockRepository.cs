using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Products;
using STS.Application.DTOs.Stock;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Infrastructure.Data;

namespace STS.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly STSDbContext _context;

        public StockRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<StockReadDto> GetByIdAsync(int id)
        {


            //STOCK ID GORE LİSTELE
            return await _context.Stocks
                                 .Where(s => s.Id == id)
                                 .Select(s => new StockReadDto {
                                     Id = s.Id,
                                     ProductId=s.ProductId,
                                     ProductName = s.Product.Name,
                                     Quantity=s.Quantity,
                                     Store=s.Store

                                 })
                                 .FirstOrDefaultAsync();
        }

        //STOCK LİSTELE
        public async Task<IEnumerable<StockReadDto>> GetAllAsync()
        {
            return await _context.Stocks
                                 .Select(s => new StockReadDto
                                 { 
                                     Id=s.Id,
                                     ProductId=s.ProductId,
                                     ProductName=s.Product.Name,
                                     Quantity=s.Quantity,
                                     Store=s.Store

                                 })
                                 .ToListAsync();
        }

        //STOCK EKLEME
        public async Task<StockReadDto> AddAsync(StockCreateDto dto)
        {
            var stock = new Stock
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                Store = dto.Store,
                Quantity = dto.Quantity

            };
            //attach()
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return new StockReadDto
            {
                Id = stock.Id,
                ProductId = stock.ProductId,
                ProductName=stock.Product.Name,
                Quantity=stock.Quantity,
                Store=stock.Store

            };


        }

        public async Task UpdateAsync(StockUpdateDto dto)
        {
            var stock = await _context.Stocks
                        .Where(stock => stock.Id == dto.Id)
                        .Select(stock => new Stock
                        {
                            Id = stock.Id,
                            Store = stock.Store,
                            Quantity = stock.Quantity


                        }).FirstOrDefaultAsync();
            if (stock == null)
                return;
            _context.Stocks.Attach(stock);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if(stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }
    }
}
