using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Products;
using STS.Application.IRepositories;
// domaindeki entityleri kullanmamiz icin gerekli "Product"
using STS.Domain.Entities;

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
            var products = await _productRepository.GetAllAsync();
            return Ok(products); // hepsi basariyla okundu
        }


        //ID GÖRE READ
        [HttpGet("ReadProductById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            //id ile eslesen urun yoksa
            if (product == null)
            {
                return NotFound();//404 bulunamadi
            }
            return Ok();//varsa basariyla okundu
        }


        //PRODUCT CREATE
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> Create(ProductCreateDto product)
        {
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        //PRODUCT UPDATE
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update( ProductUpdateDto product)
        {
           
            await _productRepository.UpdateAsync(product);
            return NoContent(); //204 basariyla guncellendi
        }


        //PRODUCT DELETE
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepository.DeleteAsync(id);
            return NoContent();//204 basariyla idli silindi
        }
    }

}
