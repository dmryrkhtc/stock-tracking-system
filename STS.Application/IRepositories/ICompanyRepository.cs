using STS.Application.DTOs.Companies;
using STS.Domain.Response;

namespace STS.Application.IRepositories
{
    public interface ICompanyRepository
    {
        //COMPANY method isimleri,islemleri
        Task<ResultResponse<CompanyReadDto>> GetByIdAsync(int id);
        Task<ResultResponse<IEnumerable<CompanyReadDto>>> GetAllAsync();
        Task<ResultResponse<CompanyReadDto>> AddAsync(CompanyCreateDto company);
        Task <ResultResponse<bool>> UpdateAsync(CompanyUpdateDto company);
        Task<ResultResponse<bool>> DeleteAsync(int id);
    }
}
