using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.Companies
{

    public class CompanyUpdateDto
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
        [Required]
        public string Address { get; set; }

        [StringLength(20)]
        [Required]
        public string TelNo { get; set; }
        [Required]

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
