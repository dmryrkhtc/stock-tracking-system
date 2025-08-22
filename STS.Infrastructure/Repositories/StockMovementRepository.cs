using STS.Domain.Entities;
using STS.Application.IRepositories;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.StockMovements;

namespace STS.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly STSDbContext _context;

        public StockMovementRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<StockMovementReadDto> GetByIdAsync(int id)
        {
            return await _context.StockMovements
                                 .Where(m=>m.Id==id)
                                 .Select(m=> new StockMovementReadDto
                                 {
                                     Id=m.Id,
                                     Quantity=m.Quantity,
                                     Date=m.Date,
                                     ProductName=m.Product.Name,
                                     Type = m.MovementType
                                 })
                                 .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<StockMovementReadDto>> GetAllAsync()
        {
            return await _context.StockMovements
                .Select(m => new StockMovementReadDto
                {
                    Id = m.Id,
                    Quantity = m.Quantity,
                    Date = m.Date,
                    ProductName = m.Product.Name,
                    Type = m.MovementType

                }).ToListAsync();
        }

        public async Task<StockMovementReadDto> AddAsync(StockMovementCreateDto dto)
        {
            var stockMovement = new StockMovement
            {
                Id = dto.Id,
                ProductId = dto.ProductId,
                MovementType = dto.Type,
                Quantity = dto.Quantity,
                Date = dto.Date
            };
            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();
            return new StockMovementReadDto {
                Id = stockMovement.Id,
                Quantity = stockMovement.Quantity,
                Date = stockMovement.Date,
                ProductName = stockMovement.Product.Name,
                Type = stockMovement.MovementType

            };
        }  


        public async Task UpdateAsync(StockMovementUpdateDto dto)
        {
            var stockMovement = await _context.StockMovements
                .Where(stockMovement => stockMovement.Id == dto.Id)
                .Select(stockMovement => new StockMovement { 
                Id=stockMovement.Id,
                MovementType=stockMovement.MovementType,
                Quantity=stockMovement.Quantity,
                Date=stockMovement.Date
                
                }).FirstOrDefaultAsync();
            if (stockMovement == null)
                return;
            _context.StockMovements.Attach(stockMovement);

        }

        public async Task DeleteAsync(int id)
        {
            var stockMovement = await _context.StockMovements.FindAsync(id);
            if(stockMovement !=null)
            {
                _context.StockMovements.Remove(stockMovement);
                await _context.SaveChangesAsync();

            }
        }
    }
}