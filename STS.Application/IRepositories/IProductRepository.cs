using STS.Application.DTOs.Products;
using STS.Domain.Entities;

namespace STS.Application.IRepositories
{
    public interface IProductRepository
    {
        //PRODUCT methodlari isimleri
        //dto read metodlari
        Task<ProductReadDto> GetByIdAsync(int id);
        Task<IEnumerable<ProductReadDto>> GetAllAsync();

        //dto create metodu
        Task<ProductReadDto> AddAsync(ProductCreateDto product);
      
        //Productcontrollerda kullandigim delete ve update metodlarini tanisin diye buraya eklendi
        //dto update metodu
        Task UpdateAsync(ProductUpdateDto product);
        Task DeleteAsync(int id);
    }
}
