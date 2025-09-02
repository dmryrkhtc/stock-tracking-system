using STS.Domain.Entities;

namespace STS.Application.DTOs.StockMovements
{
   public class StockMovementReadDto
    {
        public int Id { get; set; }
        public MovementType Type { get; set; } //girdi cikti
        public double Quantity { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get;set; } //hangi urun hareketi
        public Store Store { get; set; }
    }
}
