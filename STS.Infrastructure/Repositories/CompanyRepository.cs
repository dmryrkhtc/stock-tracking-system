using STS.Domain.Entities;
using STS.Application.IRepositories;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace STS.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly STSDbContext _context;

        public CompanyRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _context.Companies
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task AddAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public void Update(Company company)
        {
            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        public void Delete(Company company)
        {
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }
    }
}
