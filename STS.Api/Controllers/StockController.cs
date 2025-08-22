using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Stock;
using STS.Application.IRepositories;

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
        //STOCK READ
        [HttpGet("ReadStockInf")]

        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks); // tum stok bilgisi basariyla okundu
        }

        //STOCK READ BY ID
        [HttpGet("ReadStockById/{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok();
        }

        //STOCK CREATE EKLEME
        [HttpPost("CreateStock")]

        public async Task<IActionResult> Create(StockCreateDto stock)
        {
            await _stockRepository.AddAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
        }


        //STOCK UPDATE
        [HttpPut("UpdateStock")]
        public async Task<IActionResult> Update(StockUpdateDto stock)
        {
            await _stockRepository.UpdateAsync(stock);
            return NoContent();//basariyla guncellendi
        }

        //STOCK DELETE 
        [HttpDelete("DeleteStock")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stockRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
