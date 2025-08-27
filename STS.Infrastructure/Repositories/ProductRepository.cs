using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Products;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Domain.Response;
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

        // Id'ye göre ürün getir
        public async Task<ResultResponse<ProductReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.Company)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Ürün bulunamadı."
                    };
                }
                return new ResultResponse<ProductReadDto>
                {
                    Success = true,
                    Message = "Ürün başarıyla bulundu.",
                    Data = new ProductReadDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Barcode = product.Barcode,
                        Price = product.Price,
                        Unit = product.Unit,
                        CompanyName = product.Company?.Name
                    }
                };
            }


            catch (Exception ex)
            {
                return new ResultResponse<ProductReadDto>
                {
                    Success = false,
                    Message = $"Ürün getirilirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Tüm ürünleri getir
        // Tüm ürünleri getir
        public async Task<ResultResponse<IEnumerable<ProductReadDto>>> GetAllAsync()
        {
            try
            {
                var products = await _context.Products.Include(p => p.Company).ToListAsync();
                if (!products.Any())
                    return new ResultResponse<IEnumerable<ProductReadDto>>
                    {
                        Success = false,
                        Message = "Kayıtlı ürün bulunamadı."
                    };

                var readDtos = products.Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Barcode = p.Barcode,
                    Price = p.Price,
                    Unit = p.Unit,
                    CompanyName = p.Company?.Name
                }).ToList();

                return new ResultResponse<IEnumerable<ProductReadDto>>
                {
                    Success = true,
                    Message = "Ürünler başarıyla listelendi.",
                    Data = readDtos
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<ProductReadDto>>
                {
                    Success = false,
                    Message = $"Ürünler getirilirken hata oluştu: {ex.Message}"
                };
            }
        }
        // Ürün ekle
        public async Task<ResultResponse<ProductReadDto>> AddAsync(ProductCreateDto dto)
        {
            try
            {
                // Barkod kontrolü
                var exists = await _context.Products
                    .FirstOrDefaultAsync(x => x.Barcode == dto.Barcode);

                if (exists != null)
                {
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Bu barkod ile zaten bir ürün tanımlı."
                    };
                }
                // Company kontrolü
                var company = await _context.Companies.FindAsync(dto.CompanyId);
                if (company == null)
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Geçersiz şirket ID."
                    };

                // Enum parse kontrolü
                if (!Enum.TryParse<Unit>(dto.Unit.ToString(), true, out var parsedUnit))
                {
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Geçersiz birim bilgisi."
                    };
                }

                var product = new Product
                {
                    Name = dto.Name,
                    Barcode = dto.Barcode,
                    Unit = parsedUnit,
                    CompanyId = dto.CompanyId,
                    Price = dto.Price
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return new ResultResponse<ProductReadDto>
                {
                    Success = true,
                    Message = "Ürün başarıyla eklendi.",
                    Data = new ProductReadDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Barcode = product.Barcode,
                        Price = product.Price,
                        Unit = product.Unit,
                        CompanyName = product.Company?.Name,
                        CompanyId=product.CompanyId
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<ProductReadDto>
                {
                    Success = false,
                    Message = $"Ürün eklenirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Ürün güncelle
        public async Task<ResultResponse<bool>> UpdateAsync(ProductUpdateDto dto)
        {
            try
            {
                var product = await _context.Products.FindAsync(dto.Id);
                if (product == null)
                {
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Güncellenecek ürün bulunamadı."
                    };
                }

                // Enum kontrolü
                if (!Enum.TryParse<Unit>(dto.Unit.ToString(), true, out var parsedUnit))
                {
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Geçersiz birim bilgisi."
                    };
                }

                product.Name = dto.Name;
                product.Price = dto.Price;
                product.CompanyId = dto.CompanyId;
                product.Unit = parsedUnit;
                product.Barcode = dto.Barcode;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Ürün başarıyla güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Ürün güncellenirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Ürün sil
        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Silinecek ürün bulunamadı."
                    };
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Ürün başarıyla silindi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Ürün silinirken hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
