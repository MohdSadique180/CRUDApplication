using System.ComponentModel.DataAnnotations;

namespace CrudApplication.Models;

public class ProductForm
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = "";

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, 1000000, ErrorMessage = "Price must be 0 or more")]
    public decimal Price { get; set; }
}
