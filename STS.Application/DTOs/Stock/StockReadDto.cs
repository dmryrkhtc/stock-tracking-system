using STS.Domain.Entities;

namespace STS.Application.DTOs.Stock
{
    public class StockReadDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Store Store { get; set; }
        public double Quantity { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

    }
}
