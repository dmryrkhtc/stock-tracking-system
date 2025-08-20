using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Domain.Entities
{
    public enum Unit
    {
        Kg,
        Piece,
        Liter
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; } //enum degerleri 
        public Company Company { get; set; }//Nav prop
        public int CompanyId { get; set; }//fk
        public ICollection<StockMovement> StockMovements { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public Stock Stock { get; set; }
    }
}
