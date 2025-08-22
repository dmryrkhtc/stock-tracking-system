using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STS.Application.DTOs.Companies;
using STS.Application.DTOs.Users;
using STS.Application.IRepositories;
using STS.Infrastructure.Repositories;

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
            var companies = await _companyRepository.GetAllAsync();
            return Ok(companies); //hepsi basariyla okundu
        }
        [HttpGet("GetCompanyById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> Create(CompanyCreateDto company)
        {
            await _companyRepository.AddAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> Update(CompanyUpdateDto company)
        {
            await _companyRepository.UpdateAsync(company);
            return NoContent();// 204 sirket basariyla guncellendi
        }
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyRepository.DeleteAsync(id);
            return NoContent(); // sirket basariyla silindi
        }

    }
}

