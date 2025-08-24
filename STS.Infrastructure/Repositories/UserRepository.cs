using Microsoft.EntityFrameworkCore;
using STS.Application.DTOs.Users;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Domain.Response;
using STS.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly STSDbContext _context;

    public UserRepository(STSDbContext context)
    {
        _context = context;
    }

    public async Task<ResultResponse<UserReadDto>> GetByIdAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new ResultResponse<UserReadDto> { Success = false, Message = "Kullanıcı bulunamadı" };

            var readDto = new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CompanyName = user.Company.Name
            };

            return new ResultResponse<UserReadDto> { Success = true, Message = "Kullanıcı bulundu", Data = readDto };
        }
        catch (Exception ex)
        {
            return new ResultResponse<UserReadDto> { Success = false, Message = $"Hata: {ex.Message}" };
        }
    }

    public async Task<ResultResponse<IEnumerable<UserReadDto>>> GetAllAsync()
    {
        try
        {
            var users = await _context.Users.Include(u => u.Company).ToListAsync();
            if (!users.Any())
                return new ResultResponse<IEnumerable<UserReadDto>> { Success = false, Message = "Kullanıcı bulunamadı" };

            var dtos = users.Select(u => new UserReadDto
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                CompanyName = u.Company.Name
            }).ToList();

            return new ResultResponse<IEnumerable<UserReadDto>> { Success = true, Message = "Kullanıcılar listelendi", Data = dtos };
        }
        catch (Exception ex)
        {
            return new ResultResponse<IEnumerable<UserReadDto>> { Success = false, Message = $"Hata: {ex.Message}" };
        }
    }

    public async Task<ResultResponse<UserReadDto>> AddAsync(UserCreateDto dto)
    {
        try
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
            if (existingUser != null)
                return new ResultResponse<UserReadDto> { Success = false, Message = "Aynı kullanıcı mevcut" };

            var user = new User
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                CompanyId = dto.CompanyId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var readDto = new UserReadDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                CompanyName = user.Company.Name
            };

            return new ResultResponse<UserReadDto> { Success = true, Message = "Kullanıcı eklendi", Data = readDto };
        }
        catch (Exception ex)
        {
            return new ResultResponse<UserReadDto> { Success = false, Message = $"Hata: {ex.Message}" };
        }
    }

    public async Task<ResultResponse<bool>> UpdateAsync(UserUpdateInfoDto dto)
    {
        try
        {
            var user = await _context.Users.FindAsync(dto.Id);
            if (user == null)
                return new ResultResponse<bool> { Success = false, Message = "Kullanıcı bulunamadı" };

            user.Name = dto.Name;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.CompanyId = dto.CompanyId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ResultResponse<bool> { Success = true, Message = "Kullanıcı güncellendi", Data = true };
        }
        catch (Exception ex)
        {
            return new ResultResponse<bool> { Success = false, Message = $"Hata: {ex.Message}" };
        }
    }

    public async Task<ResultResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return new ResultResponse<bool> { Success = false, Message = "Kullanıcı bulunamadı" };

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new ResultResponse<bool> { Success = true, Message = "Kullanıcı silindi", Data = true };
        }
        catch (Exception ex)
        {
            return new ResultResponse<bool> { Success = false, Message = $"Hata: {ex.Message}" };
        }
    }
}
