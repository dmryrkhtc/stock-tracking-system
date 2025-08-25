using STS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace STS.Application.DTOs.Products;

public class ProductUpdateDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(120)]
    public string Name { get; set; }

    [Required]
    public int CompanyId { get; set; }
    [Required]
    public Unit Unit { get; set; } //kilo litre parca

    public double Price { get; set; } //fiyat alani
    [Required]
    public string Barcode { get; set; }


    } 

