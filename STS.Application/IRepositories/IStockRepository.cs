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
        Task<Stock> GetByIdAsync(int id);
        Task<IEnumerable<Stock>> GetAllAsync();
        Task AddAsync(Stock stock);
        void Update(Stock stock);
        void Delete(Stock stock);
    }
}
