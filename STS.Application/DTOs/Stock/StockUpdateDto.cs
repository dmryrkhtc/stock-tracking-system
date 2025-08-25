using STS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.Stock
{
   public  class StockUpdateDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public Store Store { get; set; }
        [Required]
        [Range(0,double.MaxValue,ErrorMessage ="Miktar negatif olamaz.")]
        public double Quantity { get; set; }

    }
}
