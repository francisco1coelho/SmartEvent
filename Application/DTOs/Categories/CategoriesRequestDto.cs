using System.ComponentModel.DataAnnotations;

namespace SmartEvent.Application.DTOs.Categories;

public class CategoriesRequestDto
{
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; } = string.Empty;
}