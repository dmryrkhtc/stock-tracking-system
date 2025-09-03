using System.Security.Principal;

namespace STS.Domain.Entities
{

    public enum Store
    {
        Market = 0,
        Depo = 1
    }
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; } //fk
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public Store Store { get; set; }//enum
        public string ProductName { get; set; }
   
    }
}