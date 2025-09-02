using STS.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace STS.Application.DTOs.StockMovements
{
    public class StockMovementUpdateDto
    {
        [Required]
        public int ProductId { get; set; }
        public int Id { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovementType Type { get; set; }// girdi cikti durumu
        [Required]
        public double Quantity { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Store Store { get; set; }

    }
}
