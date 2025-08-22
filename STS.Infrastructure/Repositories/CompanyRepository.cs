using STS.Domain.Entities;
using STS.Application.IRepositories;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Companies;

namespace STS.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly STSDbContext _context;

        public CompanyRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyReadDto> GetByIdAsync(int id)
        {
            return await _context.Companies
                                 .Where(c => c.Id == id)
                                 .Select(c => new CompanyReadDto
                                 {
                                     Id = c.Id,
                                     Name = c.Name,
                                     TaxNo = c.TaxNo,
                                     Address = c.Address,
                                     TelNo = c.TelNo,
                                     Email = c.Email

                                 })
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompanyReadDto>> GetAllAsync()
        {
            return await _context.Companies
                                 .Select(c => new CompanyReadDto
                                 {
                                     Id = c.Id,
                                     Name = c.Name,
                                     TaxNo = c.TaxNo,
                                     Address = c.Address,
                                     TelNo = c.TelNo,
                                     Email = c.Email
                                 })
                                 .ToListAsync();

        }

        public async Task<CompanyReadDto> AddAsync(CompanyCreateDto dto)
        {
            //c yerine company yazilabilir
            var c = new Company
            {
                Name = dto.Name,
                TaxNo = dto.TaxNo,
                Address = dto.Address,
                TelNo = dto.TelNo,
                Email = dto.Email
            };
            _context.Companies.Add(c);
            await _context.SaveChangesAsync();
            return new CompanyReadDto
            {
                Id = c.Id,
                Name = c.Name,
                TaxNo = c.TaxNo,
                Address = c.Address,
                TelNo = c.TelNo,
                Email = c.Email

            };

        }

        public async Task UpdateAsync(CompanyUpdateDto dto)
        {
            var company = await _context.Companies
                .Where(company => company.Id == dto.Id)
                .Select(company => new Company
                {
                    Id = company.Id,
                    Name = company.Name,
                    TaxNo = company.TaxNo,
                    Address = company.Address,
                    TelNo = company.TelNo,
                    Email = company.Email

                }).FirstOrDefaultAsync();
            if (company == null)
                return;
            _context.Companies.Attach(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if(company!=null)

        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        } }
    }
}
