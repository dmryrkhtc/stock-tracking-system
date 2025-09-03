using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Stock;
using STS.Application.IRepositories;
using STS.Domain.Response;

namespace STS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        // STOCK READ - ALL
        [HttpGet("ReadStockInf")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _stockRepository.GetAllAsync();

            if (!result.Success)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // STOCK READ BY ID
        [HttpGet("ReadStockById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _stockRepository.GetByIdAsync(id);

            if (!result.Success)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        // STOCK CREATE
        [HttpPost("CreateStock")]
        public async Task<IActionResult> Create([FromBody]StockCreateDto stock)
        {
            var result = await _stockRepository.AddAsync(stock);

            //if (!result.Success)
            //    return BadRequest(new { result.Message }); //400 donduruyor

            return Ok(result);
        }

        // STOCK UPDATE
        [HttpPut("UpdateStock")]
        public async Task<IActionResult> Update(StockUpdateDto stock)
        {
            var result = await _stockRepository.UpdateAsync(stock);

            return Ok(result); // 204 - başarıyla güncellendi
        }

        // STOCK DELETE
        [HttpDelete("DeleteStock/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _stockRepository.DeleteAsync(id);

          

            return Ok(result); // 204 - başarıyla silindi
        }
       
    }
}

