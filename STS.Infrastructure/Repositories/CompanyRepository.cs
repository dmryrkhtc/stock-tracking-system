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

        // ID’ye göre şirket getirme
        public async Task<ResultResponse<CompanyReadDto>> GetByIdAsync(int id)
        {
            try
            {
                var company = await _context.Companies
                                            .FirstOrDefaultAsync(c => c.Id == id);

                if (company == null)
                    return new ResultResponse<CompanyReadDto>
                    {
                        Success = false,
                        Message = "Şirket bulunamadı."
                    };

                var dto = new CompanyReadDto
                {
                    Id = company.Id,
                    Name = company.Name,
                    Email = company.Email,
                    TaxNo = company.TaxNo,
                    TelNo = company.TelNo,
                    Address = company.Address
                };

                return new ResultResponse<CompanyReadDto>
                {
                    Success = true,
                    Message = "Şirket başarıyla bulundu.",
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<CompanyReadDto>
                {
                    Success = false,
                    Message = $"Şirket getirilirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Tüm şirketleri listeleme
        public async Task<ResultResponse<IEnumerable<CompanyReadDto>>> GetAllAsync()
        {
            try
            {
                var companies = await _context.Companies.ToListAsync();

                if (!companies.Any())
                    return new ResultResponse<IEnumerable<CompanyReadDto>>
                    {
                        Success = false,
                        Message = "Kayıtlı şirket bulunamadı."
                    };

                var dtos = companies.Select(c => new CompanyReadDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    TaxNo = c.TaxNo,
                    TelNo = c.TelNo,
                    Address = c.Address
                }).ToList();

                return new ResultResponse<IEnumerable<CompanyReadDto>>
                {
                    Success = true,
                    Message = "Şirketler başarıyla listelendi.",
                    Data = dtos
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<IEnumerable<CompanyReadDto>>
                {
                    Success = false,
                    Message = $"Şirketler getirilirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Şirket ekleme
        public async Task<ResultResponse<CompanyReadDto>> AddAsync(CompanyCreateDto dto)
        {
            try
            {
                //ayni vergi numarasina sahip baska sirket var mi?
                var exists = await _context.Companies
                    .AnyAsync(c => c.TaxNo == dto.TaxNo);

                if (exists)
                {
                    return new ResultResponse<CompanyReadDto>
                    {
                        Success = false,
                        Message = "Aynı vergi numarasına ait firma mevcut!"
                    };

                }
                //ayni maile sahip sirket var mi?
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    var emailExists = await _context.Companies
                        .AnyAsync(c => c.Email == dto.Email);
                    if (emailExists)
                        return new ResultResponse<CompanyReadDto> { Success = false, Message = "Aynı e-posta adresine ait firma mevcut!" };
                }
                //yeni company olustur
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

                //readdto olustur
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
                    Message = $"Şirket eklenirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Şirket güncelleme
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
                        Message = "Güncellenecek şirket bulunamadı."
                    };
                }
                // TaxNo benzersiz mi kontrol et (kendi hariç)
                var taxNoExists = await _context.Companies
                    .AnyAsync(c => c.TaxNo == dto.TaxNo && c.Id != dto.Id);
                if (taxNoExists)
                    return new ResultResponse<bool> { Success = false, Message = "Aynı vergi numarasına sahip başka firma mevcut!" };

                // Email benzersiz mi kontrol et (kendi hariç)
                if (!string.IsNullOrEmpty(dto.Email))
                {
                    var emailExists = await _context.Companies
                        .AnyAsync(c => c.Email == dto.Email && c.Id != dto.Id);
                    if (emailExists)
                        return new ResultResponse<bool> { Success = false, Message = "Aynı e-posta adresine sahip başka firma mevcut!" };
                }
                //guncelle
                company.Name = dto.Name;
                company.TaxNo = dto.TaxNo;
                company.Address = dto.Address;
                company.TelNo = dto.TelNo;
                company.Email = dto.Email;

                await _context.SaveChangesAsync();

                return new ResultResponse<bool>
                {
                    Success = true,
                    Message = "Şirket başarıyla güncellendi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Şirket güncellenirken hata oluştu: {ex.Message}"
                };
            }
        }

        // Şirket silme
        public async Task<ResultResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var company = await _context.Companies.FindAsync(id);
                if (company == null)
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
                    Success = true,
                    Message = "Şirket başarıyla silindi.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ResultResponse<bool>
                {
                    Success = false,
                    Message = $"Şirket silinirken hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
