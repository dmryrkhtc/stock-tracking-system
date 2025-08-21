using STS.Domain.Entities;
using System.Threading.Tasks;

using System.Collections.Generic;
namespace STS.Application.IRepositories
{
    public interface IUserRepository
    {
        // USER method isimleri var sadece
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
    }
}
