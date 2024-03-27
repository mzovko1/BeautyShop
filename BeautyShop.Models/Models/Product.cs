using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautyShop.Models.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    [ValidateNever]
    public string Picture { get; set; }
    public string SkinType { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
}
