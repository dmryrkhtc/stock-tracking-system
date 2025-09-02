using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Stock;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Domain.Response;
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

        public async Task<ResultResponse<StockReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var stock = await _context.Stocks
                    .Include(s => s.Product)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (stock == null)
                    return new ResultResponse<StockReadDto>
                    {
                        Success = false,
                        Message = "Stok bulunamadı."
                    };

                var dto = new StockReadDto
                {
                    Id = stock.Id,
                    ProductId = stock.ProductId,
                    ProductName = stock.Product?.Name,
                    Quantity = stock.Quantity,
                    Store = stock.Store
                };

                return new ResultResponse<StockReadDto>
                {
                    Success = true,
                    Message = "Stok başarıyla bulundu.",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<StockReadDto>
                {
                    Success = false,
                    Message = $"Stok getirilirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<IEnumerable<StockReadDto>>> GetAllAsync()
        {
            try
            {
                var stocks = await _context.Stocks
                    .Include(s => s.Product)
                    .ToListAsync();

                var dtos = stocks.Select(s => new StockReadDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    ProductName = s.Product?.Name,
                    Quantity = s.Quantity,
                    Store = s.Store
                }).ToList();

                return new ResultResponse<IEnumerable<StockReadDto>>
                {
                    Success = true,
                    Message = "Stoklar başarıyla listelendi.",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<StockReadDto>>
                {
                    Success = false,
                    Message = $"Stoklar getirilirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<StockReadDto>> AddAsync(StockCreateDto dto)
        {
            try
            {
                var product = await _context.Products.FindAsync(dto.ProductId);
                if (product == null)
                    return new ResultResponse<StockReadDto>
                    {
                        Success = false,
                        Message = "Geçersiz ürün ID."
                    };

                if (!Enum.TryParse<Store>(dto.Store, true, out var storeEnum))
                    return new ResultResponse<StockReadDto>
                    {
                      

                    };

                var stock = await _context.Stocks
                    .FirstOrDefaultAsync(s => s.ProductId == dto.ProductId && s.Store == storeEnum);

                if (stock != null)
                {
                    return new ResultResponse<StockReadDto>
                    {
                        Success = false,
                        Message = "Bu depoda zaten aynı ürün mevcut."
                    };
                }

                stock = new Stock
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    Store = storeEnum,
                    ProductName = product.Name
                };

                _context.Stocks.Add(stock);
                await _context.SaveChangesAsync();

                var dtoRead = new StockReadDto
                {
                    Id = stock.Id,
                    ProductId = dto.ProductId,
                    ProductName = product.Name,
                    Quantity = stock.Quantity,
                    Store = stock.Store
                };

                return new ResultResponse<StockReadDto>
                {
                    Success = true,
                    Message = "Stok başarıyla eklendi.",
                    Data = dtoRead
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<StockReadDto>
                {
                    Success = false,
                    Message = $"Hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<bool>> UpdateAsync(StockUpdateDto dto)
        {
            try
            {
                if (dto.Quantity < 0)
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Miktar negatif olamaz."
                    };

                var stock = await _context.Stocks.FindAsync(dto.Id);
                if (stock == null)
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Güncellenecek stok bulunamadı."
                    };

                // Eğer depo değişmişse aynı depoda ürün var mı kontrol et
                if (stock.Store != dto.Store)
                {
                    var targetStock = await _context.Stocks
                        .FirstOrDefaultAsync(s => s.ProductId == stock.ProductId && s.Store == dto.Store);

                    if (targetStock != null)
                        return new ResultResponse<bool>
                        {
                            Success = false,
                            Message = "Hedef depoda zaten aynı ürün var."
                        };

                    stock.Store = dto.Store; // Depo değiştirilebilir
                }

                stock.Quantity = dto.Quantity;

                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Stok başarıyla güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Stok güncellenirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var stock = await _context.Stocks.FindAsync(id);
                if (stock == null)
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Silinecek stok bulunamadı."
                    };

                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Stok başarıyla silindi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Stok silinirken hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
