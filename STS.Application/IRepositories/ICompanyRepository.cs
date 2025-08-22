using STS.Application.DTOs.Companies;
using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.IRepositories
{
    public interface ICompanyRepository
    {
        //COMPANY method isimleri,islemleri
        Task<CompanyReadDto> GetByIdAsync(int id);
        Task<IEnumerable<CompanyReadDto>> GetAllAsync();
        Task<CompanyReadDto> AddAsync(CompanyCreateDto company);
        Task UpdateAsync(CompanyUpdateDto company);
        Task DeleteAsync(int id);
    }
}
