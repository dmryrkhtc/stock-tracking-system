using STS.Domain.Entities; //movementtype bulunsun diye
using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.StockMovements
{
    public class StockMovementCreateDto
    {
  
        [Required]
        public int ProductId { get; set; }
        [Required]
        public MovementType Type { get; set; } //girdi cikti

        [Required]
        public double Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Store Store { get; set; } // kullanici hangi depoya ekleyecegini secsin

    }
}
