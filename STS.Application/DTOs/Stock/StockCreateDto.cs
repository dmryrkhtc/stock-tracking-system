using STS.Domain.Entities;
using System.ComponentModel.DataAnnotations;//***arastir

namespace STS.Application.DTOs.Stock
{
    public class StockCreateDto
    {

        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Store { get; set; }//string enum


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalı.")]
        public double Quantity { get; set; }


    }
}