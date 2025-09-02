using STS.Application.DTOs.StockMovements;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STS.Domain.Response;

namespace STS.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly STSDbContext _context;

        public StockMovementRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<ResultResponse<StockMovementReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var movement = await _context.StockMovements
                                             .Include(m => m.Product)
                                             .Where(m => m.Id == id)
                                             .Select(m => new StockMovementReadDto
                                             {
                                                 Id = m.Id,
                                                 Quantity = m.Quantity,
                                                 Date = m.Date,
                                                 ProductName = m.Product.Name,
                                                 Type = m.MovementType,
                                                 Store=m.Store
                                             })
                                             .FirstOrDefaultAsync();

                if (movement == null)
                    return new ResultResponse<StockMovementReadDto>
                    {
                        Success = false,
                        Message = "Stok hareketi bulunamadı."
                    };

                return new ResultResponse<StockMovementReadDto>
                {
                    Success = true,
                    Message = "Stok hareketi başarıyla bulundu.",
                    Data = movement
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<StockMovementReadDto>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<IEnumerable<StockMovementReadDto>>> GetAllAsync()
        {
            try
            {
                var movements = await _context.StockMovements
                    .Include(m => m.Product)
                    .Select(m => new StockMovementReadDto
                    {
                        Id = m.Id,
                        Quantity = m.Quantity,
                        Date = m.Date,
                        ProductName = m.Product.Name,
                        Type = m.MovementType,
                        Store=m.Store
                        
                    }).ToListAsync();

                return new ResultResponse<IEnumerable<StockMovementReadDto>>
                {
                    Success = true,
                    Message = "Stok hareketleri başarıyla listelendi.",
                    Data = movements
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<StockMovementReadDto>>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<StockMovementReadDto>> AddAsync(StockMovementCreateDto dto)
        {
            try
            {
                var product = await _context.Products.FindAsync(dto.ProductId);
                if (product == null)
                    return new ResultResponse<StockMovementReadDto> { Success = false, Message = "Geçersiz ürün ID." };

                // Stok kontrolü: ürün ve depo
                var stock = await _context.Stocks
                                          .FirstOrDefaultAsync(s => s.ProductId == dto.ProductId && s.Store == dto.Store);

                if (dto.Type == MovementType.Exit) // çıkış işlemi
                {
                    if (stock == null || stock.Quantity < dto.Quantity)
                        return new ResultResponse<StockMovementReadDto> { Success = false, Message = "Yetersiz stok." };

                    stock.Quantity -= dto.Quantity; // stok azaltılıyor
                }
                else // Entry, giriş işlemi
                {
                    if (stock != null)
                        stock.Quantity += dto.Quantity; // mevcut stok artıyor
                    else
                        _context.Stocks.Add(new Stock { ProductId = dto.ProductId, Quantity = dto.Quantity, Store = dto.Store });
                }

                //  Stok hareketini ekle
                var movement = new StockMovement
                {
                    ProductId = dto.ProductId,
                    MovementType = dto.Type,
                    Quantity = dto.Quantity,
                    Date = dto.Date,
       
                    Store=dto.Store
                };
                _context.StockMovements.Add(movement);


                await _context.SaveChangesAsync();

                var readDto = new StockMovementReadDto
                {
                    Id = movement.Id,
                    Quantity = movement.Quantity,
                    Date = movement.Date,
                    ProductName = product?.Name,
                    Type = movement.MovementType
                };

                return new ResultResponse<StockMovementReadDto>
                {
                    Success = true,
                    Message = "Stok hareketi başarıyla eklendi.",
                    Data = readDto
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<StockMovementReadDto>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<bool>> UpdateAsync(StockMovementUpdateDto dto)
        {
            try
            {
                var movement = await _context.StockMovements.FindAsync(dto.Id);
                if (movement == null)
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Güncellenecek stok hareketi bulunamadı."
                    };

                movement.Quantity = dto.Quantity;
                movement.MovementType = dto.Type;
                movement.Date = dto.Date;

                _context.StockMovements.Update(movement);
                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Stok hareketi başarıyla güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var movement = await _context.StockMovements.FindAsync(id);
                if (movement == null)
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Silinecek stok hareketi bulunamadı."
                    };
                // Stok düzeltmesi
                var stock = await _context.Stocks
                          .FirstOrDefaultAsync(s => s.ProductId == movement.ProductId && s.Store == movement.Store);

                if (stock != null)
                {
                    if (movement.MovementType == MovementType.Entry)
                        stock.Quantity -= movement.Quantity; // giriş hareketi silindiğinde stok azalır
                    else
                        stock.Quantity += movement.Quantity; // çıkış hareketi silindiğinde stok artar
                }

                _context.StockMovements.Remove(movement);
                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Stok hareketi başarıyla silindi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
