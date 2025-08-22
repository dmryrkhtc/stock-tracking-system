using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.Companies
{
    public class CompanyCreateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string TaxNo { get; set; }

        
        [StringLength(250)]
        public string Address { get; set; }

        
        [StringLength(20)]
        public string TelNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }
}
