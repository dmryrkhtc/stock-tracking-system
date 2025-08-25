using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.Companies
{
    public class CompanyCreateDto
    {
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string TaxNo { get; set; }

        
        [StringLength(250)]
        public string Address { get; set; }
        [Required]

        [StringLength(20)]
        public string TelNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
