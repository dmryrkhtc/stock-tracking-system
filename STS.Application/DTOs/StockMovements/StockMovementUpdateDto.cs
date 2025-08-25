using STS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.StockMovements
{
    public class StockMovementUpdateDto
    {
        [Required]
        public int ProductId { get; set; }
        public int Id { get; set; }
        [Required]
        public MovementType Type { get; set; }// girdi cikti durumu
        [Required]
        public double Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Store Store { get; set; }

    }
}
