using STS.Domain.Entities;
using STS.Application.IRepositories;
using STS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Users;
using System.Reflection.Metadata.Ecma335;

namespace STS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly STSDbContext _context;

        public UserRepository(STSDbContext context)
        {
            _context = context;
        }

        public async Task<UserReadDto> GetByIdAsync(int id)
        {
            return await _context.Users
                                 .Where(u => u.Id == id)
                                 .Select(u => new UserReadDto
                                 {
                                     Id = u.Id,
                                     Name= u.Name,
                                     LastName= u.LastName,
                                     Email= u.Email,
                                     CompanyName=u.Company.Name

                                 })
                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserReadDto>> GetAllAsync()
        {
            return await _context.Users
                                 .Select(u => new UserReadDto
                                 {
                                     Id = u.Id,
                                     Name = u.Name,
                                     LastName = u.LastName,
                                     Email = u.Email,
                                     CompanyName = u.Company.Name
                                 })
                                 .ToListAsync();
        }

        public async Task<UserReadDto> AddAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                CompanyId = dto.CompanyId
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserReadDto
            {
                Id=user.Id,
                Name=user.Name,
                LastName=user.LastName,
                Email=user.Email,
                CompanyName=user.Company.Name

            };
        }
     


        public async Task UpdateAsync(UserUpdateInfoDto userInfo)
        {
            var vUser = await _context.Users
                .Where(user => user.Id == userInfo.Id)
                .Select(user => new User
                {
                    Id= user.Id,
                    Name= user.Name,
                    LastName=user.LastName,
                    Email=user.Email
                }).FirstOrDefaultAsync();

            if (vUser == null)
                return;

            // vuser diye tablo yaptim ondaki degisiklik veritabindakine denk gelecek
            _context.Users.Attach(vUser);

            vUser.Name = userInfo.Name;
            vUser.LastName = userInfo.LastName;
            vUser.Email = userInfo.Email;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var user = await _context.Users.FindAsync(id);
            if (user!=null)
        {
            _context.Users.Remove(user);
           await _context.SaveChangesAsync();
        } }
    }
}
