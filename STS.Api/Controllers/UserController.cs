using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Users;
using STS.Application.IRepositories;
using STS.Domain.Entities;

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
            var users = await _userRepository.GetAllAsync();
            return Ok(users); //hepsi basariyla okundu
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> Create(UserCreateDto user)
        {
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> Update(UserUpdateInfoDto user)
        {
            await _userRepository.UpdateAsync(user);
            return NoContent();// 204 kullanici basariyla guncellendi
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepository.DeleteAsync(id);
            return NoContent(); // kullanici basariyla silin
        }

    } }
