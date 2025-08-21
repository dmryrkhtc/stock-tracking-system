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
        Task<StockMovement> GetByIdAsync(int id);
        Task<IEnumerable<StockMovement>> GetAllAsync();
        Task AddAsync(StockMovement stockMovement);
        void Update(StockMovement stockMovement);
        void Delete(StockMovement stockMovement);
    }
}
