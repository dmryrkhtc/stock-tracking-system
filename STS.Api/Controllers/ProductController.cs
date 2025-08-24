using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Products;
using STS.Application.IRepositories;


namespace STS.Api.Controllers
//restapi route
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //PRODUCT READ 
        [HttpGet("ReadProductSummary")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRepository.GetAllAsync();
            if (!result.Success)
                return NotFound(new { result.Message });
            return Ok(result.Data);
        }


        //ID GÖRE READ
        [HttpGet("ReadProductById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            //id ile eslesen urun yoksa
            if (!result.Success)
            {
                return NotFound(new {result.Message});//404 bulunamadi
            }
            return Ok(result.Data);//varsa basariyla okundu
        }


        //PRODUCT CREATE
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(ProductCreateDto product)
        {
            var result = await _productRepository.AddAsync(product);
            if (!result.Success)
            { 
                return BadRequest(new { result.Message });
            }
            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data.Id },//yeni eklenen ürün idsi
                result.Data//yeni eklenen ürün dtosu
                );
        }


        //PRODUCT UPDATE
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update( ProductUpdateDto product)
        {
            var result = await _productRepository.UpdateAsync(product);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();//basarıyla guncellendi
        }


        //PRODUCT DELETE
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productRepository.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();
        }
    }

}
