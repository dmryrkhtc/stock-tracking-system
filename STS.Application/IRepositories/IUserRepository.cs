using STS.Domain.Entities;
using System.Threading.Tasks;

using System.Collections.Generic;
using STS.Application.DTOs.Users;
namespace STS.Application.IRepositories
{
    public interface IUserRepository
    {
        // USER method isimleri var sadece
        Task<UserReadDto> GetByIdAsync(int id);
        Task<IEnumerable<UserReadDto>> GetAllAsync();
        Task<UserReadDto> AddAsync(UserCreateDto user);
        Task UpdateAsync(UserUpdateInfoDto user);
        Task DeleteAsync(int Id);
    }
}
