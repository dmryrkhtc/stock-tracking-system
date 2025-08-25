using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Users;
using STS.Application.IRepositories;
using STS.Domain.Entities;
using STS.Infrastructure.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace STS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //USER READ
        [HttpGet("GetUserSummary")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRepository.GetAllAsync();
            if (!result.Success)
                return NotFound(new { result.Message });
            return Ok(result.Data);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create(UserCreateDto user)
        {
            var result = await _userRepository.AddAsync(user);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data.Id },//yeni eklenen kullanici idsi
                result.Data//yeni eklenen kullanici dtosu
            );
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update(UserUpdateInfoDto user)
        {
            var result = await _userRepository.UpdateAsync(user);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();// 204 kullanıcı basariyla guncellendi
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userRepository.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(new
                {
                    result.Message
                });
           
            return NoContent(); // kullanici basariyla silindi
        }

    } }
