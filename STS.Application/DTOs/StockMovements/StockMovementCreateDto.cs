using STS.Domain.Entities; //movementtype bulunsun diye
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace STS.Application.DTOs.StockMovements
{
    public class StockMovementCreateDto
    {
  
        [Required]
        public int ProductId { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovementType Type { get; set; } //girdi cikti

        [Required]
        public double Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Store Store { get; set; } // kullanici hangi depoya ekleyecegini secsin
    
     

    }
}
