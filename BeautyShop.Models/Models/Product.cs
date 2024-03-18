using System.ComponentModel.DataAnnotations;

namespace BeautyShop.Models.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Picture { get; set; }
    public string SkinType { get; set; }
    public Category Category { get; set; }
}
