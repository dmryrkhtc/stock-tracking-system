using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.Stock
{
   public  class StockUpdateDto
    {
        [Required]
        public Store Store { get; set; }
        [Required]
        public double Quantity { get; set; }

    }
}
