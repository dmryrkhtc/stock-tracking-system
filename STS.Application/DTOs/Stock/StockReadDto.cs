using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.Stock
{
    class StockReadDto
    {
        public int Id { get; set; }
        public int ProductId { get; set;}
        public string ProductName { get; set; }
        public Store Store { get; set; }
        public double Quantity { get; set; }
    }
}
