using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Application.DTOs.StockMovements
{
    public class StockMovementUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public MovementType Type { get; set; }// girdi cikti durumu
        [Required]
        public double Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }

    }
}
