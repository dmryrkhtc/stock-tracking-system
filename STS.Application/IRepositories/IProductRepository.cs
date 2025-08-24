using STS.Application.DTOs.Products;
using STS.Domain.Entities;
using STS.Domain.Response;

namespace STS.Application.IRepositories
{
    public interface IProductRepository
    {
        //PRODUCT methodlari isimleri
        //dto read metodlari
        Task<ResultResponse<ProductReadDto>> GetByIdAsync(int id);
        Task<ResultResponse<IEnumerable<ProductReadDto>>> GetAllAsync();

        //dto create metodu
        Task<ResultResponse<ProductReadDto>> AddAsync(ProductCreateDto product);

        //Productcontrollerda kullandigim delete ve update metodlarini tanisin diye buraya eklendi
        //dto update metodu
        Task<ResultResponse<bool>> UpdateAsync(ProductUpdateDto product);
        Task<ResultResponse<bool>> DeleteAsync(int id);
    }
}
