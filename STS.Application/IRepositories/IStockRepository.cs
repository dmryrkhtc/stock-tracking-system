using STS.Application.DTOs.Stock;
using STS.Domain.Entities;
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
        Task<StockReadDto> GetByIdAsync(int id);
        Task<IEnumerable<StockReadDto>> GetAllAsync();
        Task<StockReadDto> AddAsync(StockCreateDto stock);
        Task UpdateAsync(StockUpdateDto stock);
        Task DeleteAsync(int id);
    }
}
