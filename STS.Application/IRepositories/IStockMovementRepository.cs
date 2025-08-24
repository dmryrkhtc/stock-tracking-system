using STS.Application.DTOs.StockMovements;
using STS.Domain.Entities;
using STS.Domain.Response;
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
        Task<ResultResponse<StockMovementReadDto>> GetByIdAsync(int id);
        Task<ResultResponse<IEnumerable<StockMovementReadDto>>> GetAllAsync();
        Task<ResultResponse<StockMovementReadDto>> AddAsync(StockMovementCreateDto stockMovement);
        Task <ResultResponse<bool>> UpdateAsync(StockMovementUpdateDto stockMovement);
        Task<ResultResponse<bool>> DeleteAsync(int id);
    }
}
