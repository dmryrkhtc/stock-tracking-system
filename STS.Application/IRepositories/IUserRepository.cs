using STS.Application.DTOs.Users;
using STS.Domain.Response;
namespace STS.Application.IRepositories
{
    public interface IUserRepository
    {
        // USER method isimleri var sadece
        Task<ResultResponse<UserReadDto>> GetByIdAsync(int id);
        Task<ResultResponse<IEnumerable<UserReadDto>>> GetAllAsync();
        Task<ResultResponse<UserReadDto>> AddAsync(UserCreateDto user);
        Task <ResultResponse<bool>>UpdateAsync(UserUpdateInfoDto user);
        Task<ResultResponse<bool>> DeleteAsync(int Id);
    }
}
