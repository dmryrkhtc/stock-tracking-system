using STS.Domain.Entities;

namespace STS.Application.DTOs.Products
{
   public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }//urunler hangi sirketten
        public int CompanyId { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public double Price { get; set; }
        public string Barcode { get; set; }
    }
}
