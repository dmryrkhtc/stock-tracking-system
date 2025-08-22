using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Products;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Infrastructure.Data;

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
        public async Task<ProductReadDto> GetByIdAsync(int id)
        {
            return await _context.Products
                                 .Where(p => p.Id == id)
                                 .Select(p => new ProductReadDto
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Price = p.Price,
                                     CompanyName = p.Company.Name,
                                     // enum ile sayisal deger olarak tuttugumuz birim degerlerini string yaptik
                                     Unit = p.Unit


                                 })
                                 .FirstOrDefaultAsync();
        }
        // tum urunleri okuduk 
        public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
        {
            return await _context.Products
                                 .Select(p => new ProductReadDto
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     Price = p.Price,
                                     CompanyName = p.Company.Name,
                                     Unit = p.Unit

                                 })
                                 .ToListAsync();
        }

        // Urun ekleme(createdto alir ve readdto dondurur)
        public async Task<ProductReadDto> AddAsync(ProductCreateDto dto)
        {

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
               CompanyId=dto.CompanyId,
                Unit = dto.Unit
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductReadDto
            {
                // burada neden dto kullanmadik
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CompanyName = product.Company.Name,
                Unit = product.Unit

            };
        }

        //guncelleme 
        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var product = await _context.Products
                  .Where(product => product.Id == dto.Id)
                  .Select(product => new Product
                  {
                      Id = product.Id,
                      Name = product.Name,
                      Price = product.Price,
                      Unit = product.Unit


                  }).FirstOrDefaultAsync();
            if (product == null)
                return;
            _context.Products.Attach(product);

        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }


        }

    }
}
