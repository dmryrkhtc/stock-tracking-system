using STS.Application.DTOs.Stock;
using STS.Domain.Entities;
using STS.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.IRepositories
{
     public interface IStockRepository
    {
        //STOCK metod isimleri
        Task<ResultResponse<StockReadDto>> GetByIdAsync(int id);
        Task<ResultResponse<IEnumerable<StockReadDto>>> GetAllAsync();
        Task<ResultResponse<StockReadDto>> AddAsync(StockCreateDto stock);
        Task<ResultResponse<bool>> UpdateAsync(StockUpdateDto stock);
        Task <ResultResponse< bool>>DeleteAsync(int id);
        
    }
}
