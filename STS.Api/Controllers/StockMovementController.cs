using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using STS.Application.DTOs.Stock;
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

        //STOCKMOVEMENT READ 
        [HttpGet("ReadStockMovement")]

        public async Task<IActionResult> GetAll()
        {
            var stockMovements = await _stockMovementRepository.GetAllAsync();
            return Ok(stockMovements);//tum stok hareket bilgisi okundu
        }

        //STOCKMOVEMENT ID GORE READ
        [HttpGet("ReadStockMovementById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
        var stockMovement = await _stockMovementRepository.GetByIdAsync(id);
            if(stockMovement==null)
            {
                return NotFound();
            }
            return Ok(stockMovement);

}
        //STOCKMOVEMENT CREATE 
        [HttpPost("CreateStockMovement")]
        public async Task<IActionResult> Create(StockMovementCreateDto stockMovement)
        {
            await _stockMovementRepository.AddAsync(stockMovement);
            return CreatedAtAction(nameof(GetById), new { id = stockMovement.Id }, stockMovement);
        
        
        }

        //STOCKMOVEMENT UPDATE
        [HttpPut("UpdateStockMovement")]
        public async Task<IActionResult> Update(StockMovementUpdateDto stockMovement)
        {
            await _stockMovementRepository.UpdateAsync(stockMovement);
            return NoContent();
        }

        //STOCKMOVEMENT DELETE
        [HttpDelete("DeleteStockMovement")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stockMovementRepository.DeleteAsync(id);
            return NoContent();
        }



    }
}
