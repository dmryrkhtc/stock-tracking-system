using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.StockMovements;
using STS.Application.IRepositories;

namespace STS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly IStockMovementRepository _stockMovementRepository;

        public StockMovementController(IStockMovementRepository stockMovementRepository)
        {
            _stockMovementRepository = stockMovementRepository;
        }

        [HttpGet("ReadStockMovement")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stockMovementRepository.GetAllAsync();
            if (!result.Success)
                return NotFound(new { result.Message });
            return Ok(result.Data);
        }

        [HttpGet("ReadStockMovementById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _stockMovementRepository.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(new { result.Message });
            return Ok(result.Data);
        }

        [HttpPost("CreateStockMovement")]
        public async Task<IActionResult> Create(StockMovementCreateDto dto)
        {
            var result = await _stockMovementRepository.AddAsync(dto);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("UpdateStockMovement")]
        public async Task<IActionResult> Update(StockMovementUpdateDto dto)
        {
            var result = await _stockMovementRepository.UpdateAsync(dto);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();
        }

        [HttpDelete("DeleteStockMovement/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stockMovementRepository.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();
        }
    }
}
