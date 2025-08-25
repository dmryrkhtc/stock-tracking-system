using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Domain.Entities
{
    public enum MovementType
    {
        Entry,
        Exit
    }
       
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; } //fk
        public Product Product { get; set; } //nav prop
        public MovementType MovementType { get; set; }
        public double Quantity { get; set; } //+10 giris -5 cikis
        public DateTime Date { get; set; }
        public Store Store { get; set; } //stok hareketi hangi depoya bagli

    }
}
