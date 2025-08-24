using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Companies;
using STS.Application.IRepositories;

namespace STS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        //company READ
        [HttpGet("GetCompanySummary")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyRepository.GetAllAsync();
            if (!result.Success)
                return NotFound(new { result.Message });
            return Ok(result.Data);//200+sirket listesi
        }
        [HttpGet("GetCompanyById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _companyRepository.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(new { result.Message });
           
            return Ok(result.Data);//200+ilgili sirket
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> Create(CompanyCreateDto company)
        {
            var result = await _companyRepository.AddAsync(company);

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data.Id },//yeni eklenen sirket idsi
                result.Data//yeni eklenen sirket dtosu
            );
        }


        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> Update(CompanyUpdateDto company)
        {
            var result =await _companyRepository.UpdateAsync(company);
            if (!result.Success)
                return BadRequest(new { result.Message });
            return NoContent();// 204 sirket basariyla guncellendi
        }
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           var result= await _companyRepository.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(new {result.Message
                });
            return NoContent(); // sirket basariyla silindi
        }

    }
}

