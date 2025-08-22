using STS.Application.DTOs.StockMovements;
using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.IRepositories
{
    public interface IStockMovementRepository
    {
        //STOKHAREKETLERİ metod isimleri
        Task<StockMovementReadDto> GetByIdAsync(int id);
        Task<IEnumerable<StockMovementReadDto>> GetAllAsync();
        Task<StockMovementReadDto> AddAsync(StockMovementCreateDto stockMovement);
        Task  UpdateAsync(StockMovementUpdateDto stockMovement);
        Task DeleteAsync(int id);
    }
}
