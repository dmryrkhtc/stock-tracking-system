using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Companies;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Domain.Response;
using STS.Infrastructure.Data;

namespace STS.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly STSDbContext _context;

        public CompanyRepository(STSDbContext context)
        {
            _context = context;
        }


        //ID GORE SİRKET GETİRME RESULT RESPONSE İLE
        public async Task<ResultResponse<CompanyReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if (company == null)
                {
                    return new ResultResponse<CompanyReadDto>
                    {
                        Success = false,
                        Message = "Şirket Bulunamadı."
                    };
                }

                var readDto = new CompanyReadDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Email = company.Email,
                    TaxNo = company.TaxNo,
                    TelNo = company.TelNo
                };
                return new ResultResponse<CompanyReadDto>
                {
                    Success = true,
                    Message = "Şirket başarıyla bulundu.",
                    Data = readDto
                };
            }
            catch(Exception ex)
            {
                return new ResultResponse<CompanyReadDto>
                {
                    Success = false,
                    Message = $"Şirket getirilirken hata oluştu:{ex.Message}"

                };
            }
            }

        public async Task<ResultResponse<IEnumerable<CompanyReadDto>>> GetAllAsync()
        {
            try
            {
                var companies = await _context.Companies.ToListAsync();
                if (!companies.Any())
                {
                    return new ResultResponse<IEnumerable<CompanyReadDto>>
                    {
                        Success = false,
                        Message = "Kayıtlı şirket bulunamadı."
                    };

                }
                var readDtos = companies.Select(c=> new CompanyReadDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    TaxNo = c.TaxNo,
                    TelNo = c.TelNo
                }).ToList();
                return new ResultResponse<IEnumerable<CompanyReadDto>>
                {
                    Success = true,
                    Message="Şirket başarıyla listelendi.",
                    Data=readDtos

                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<CompanyReadDto>>
                {
                    Success = false,
                    Message = $"Şirketler getirilirken hata oluştu:{ex.Message}"
                };
            }

        }

        public async Task<ResultResponse<CompanyReadDto>> AddAsync(CompanyCreateDto dto)
        {
            try
            {
                var vCompanies = await _context.Companies
                    .FirstOrDefaultAsync(x => x.TaxNo == dto.TaxNo);

                if (vCompanies != null)
                {
                    return new ResultResponse<CompanyReadDto>
                    {
                        Success = false,
                        Message = "Aynı vergi numarasına ait firma mevcut!",
                        Data = null
                    };
                }

                var company = new Company
                {
                    Name = dto.Name,
                    TaxNo = dto.TaxNo,
                    Address = dto.Address,
                    TelNo = dto.TelNo,
                    Email = dto.Email
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                var dtoRead = new CompanyReadDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    TaxNo = company.TaxNo,
                    Address = company.Address,
                    TelNo = company.TelNo,
                    Email = company.Email
                };

                return new ResultResponse<CompanyReadDto>
                {
                    Success = true,
                    Message = "Firma başarıyla eklendi.",
                    Data = dtoRead
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<CompanyReadDto>
                {
                    Success = false,
                    Message = $"Şirket eklenirken hata oluştu: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<ResultResponse<bool>> UpdateAsync(CompanyUpdateDto dto)
        {
            try
            {
                var company = await _context.Companies.FindAsync(dto.Id);
                if (company == null)
                {
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Güncellenecek şirket bulunmadı."
                    };
                }
                company.Name = dto.Name;
                company.TaxNo = dto.TaxNo;
                company.Address = dto.Address;
                company.TelNo = dto.TelNo;
                company.Email = dto.Email;
                _context.Companies.Update(company);
                await _context.SaveChangesAsync();
                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Şirket başarıyla güncellendi.",
                    Data = true
                };
            }
            catch(Exception ex)
            {
                return new ResultResponse<bool>
                { 
                    Success=false,
                    Message=$"Şirket güncellenirken hata oluştu:{ex.Message}"
            

           };}
        }

        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if(company == null)
                {
                    return new ResultResponse<bool>
                    {
                        Success = false,
                        Message = "Silinecek şirket bulunamadı."
                    };
                }
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                return new ResultResponse<bool>
                {
                    Success=true,
                    Message="Şirket başarıyla silindi.",
                    Data=true

                };


            }
            catch( Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success=false,
                    Message=$"Şirket silinirken hata oluştu:{ex.Message}"
                };
            }
        }
    }
}
