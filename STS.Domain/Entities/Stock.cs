namespace STS.Domain.Entities
{

    public enum Store
    {
        Store1,
        Store2
    }
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; } //fk
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public Store Store { get; set; }//enum
    }
}