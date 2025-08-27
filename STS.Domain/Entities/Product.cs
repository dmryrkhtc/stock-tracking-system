using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Domain.Entities
{
    public enum Unit
    {
        Kg=0,
        Piece=2,
        Liter=1
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public Unit Unit { get; set; } //enum
        public int CompanyId { get; set; } // FK
        public Company Company { get; set; } // Nav prop
        public ICollection<Stock> Stocks { get; set; } // one-to-many
        public ICollection<StockMovement> StockMovements { get; set; }
    }
}
