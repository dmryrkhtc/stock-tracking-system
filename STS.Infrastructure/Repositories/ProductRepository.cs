using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Products;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Domain.Response;
using STS.Infrastructure.Data;
using System.Reflection.Metadata;

namespace STS.Infrastructure.Repositories
{
    //servis
    public class ProductRepository : IProductRepository
    {
        private readonly STSDbContext _context;

        public ProductRepository(STSDbContext context)
        {
            _context = context;
        }


        // idye gore urun okuma
        public async Task<ResultResponse<ProductReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Ürün Bulunamadı."
                    };
                }
                var readDto = new ProductReadDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CompanyName = product.Company.Name,
                    // enum ile sayisal deger olarak tuttugumuz birim degerlerini string yaptik
                    Unit = product.Unit
                };
                return new ResultResponse<ProductReadDto>
                {
                    Success = true,
                    Message = "Ürün başarıyla bulundu.",
                    Data = readDto
                };
            }
            catch(Exception ex)
            {
                return new ResultResponse<ProductReadDto> {
                Success=false,
                Message=$"Ürün getirilirken hata oluştu: {ex.Message}"
                };
            }
        }
        // tum urunleri okuduk 
        public async Task<ResultResponse<IEnumerable<ProductReadDto>>> GetAllAsync()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (!products.Any())
                {
                    return new ResultResponse<IEnumerable<ProductReadDto>> { 
                    Success=false,
                    Message="Kayıtlı ürün bulunamadı"
                    };
                }
                var readDtos = products.Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CompanyName = p.Company.Name,
                    Unit = p.Unit

                }).ToList();
                return new ResultResponse<IEnumerable<ProductReadDto>>
                {
                    Success = true,
                    Message = "Ürün başarıyla listelendi.",
                    Data = readDtos

                };
            }
            catch(Exception ex)
            {
                return new ResultResponse<IEnumerable<ProductReadDto>> { 
                Success=false,
                Message=$"Ürün getirilirken hata oluştu:{ex.Message}"
                };
            }
        }

        // Urun ekleme(createdto alir ve readdto dondurur)
        public async Task<ResultResponse<ProductReadDto>> AddAsync(ProductCreateDto dto)
        {
            try
            {
                var exists = await _context.Products
                    .FirstOrDefaultAsync(x => x.Barcode == dto.Barcode); //ayni barkodlu urun mu

                if (exists != null)
                {
                    return new ResultResponse<ProductReadDto>
                    {
                        Success = false,
                        Message = "Bu barkod ile zaten bir ürün tanımlı."
                    };
                }

                var product = new Product
                {
                    Name = dto.Name,
                    Barcode = dto.Barcode,
                    Unit = dto.Unit,
                    CompanyId = dto.CompanyId
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
                        CompanyName = product.Company?.Name
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<ProductReadDto>
                {
                    Success = false,
                    Message = $"Ürün eklenirken hata: {ex.Message}"
                };
            }
        }

        //guncelleme 
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
                product.Name = dto.Name;
                product.Price = dto.Price;
                product.CompanyId = dto.CompanyId;
                product.Unit = dto.Unit;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return new ResultResponse<bool>
                {
                    Success=true,
                    Message="Ürün bilgileri başarıyla güncellendi.",
                    Data=true
                };


            }
            catch(Exception ex)
            {
                return new ResultResponse<bool> { 
                Success=false,
                Message=$"Ürün güncellenirken hata oluştu:{ex.Message}"
                };
            }

        }

        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        { try
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
            catch(Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Ürün silinirken hata oluştu:{ex.Message}"
                };
            }

        }

    }
}
