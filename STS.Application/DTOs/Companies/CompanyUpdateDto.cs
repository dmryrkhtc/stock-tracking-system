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
