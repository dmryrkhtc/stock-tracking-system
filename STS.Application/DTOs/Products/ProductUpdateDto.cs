using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.Products
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        public Unit Unit { get; set; } //kilo litre parca

        public double Price { get; set; } //fiyat alani
       

    } 
}
