// Application/Services/ICategoryService.cs
using SmartEvent.Application.DTOs.Categories;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(CategoriesRequestDto dto);
    Task<Category> UpdateCategoryAsync(int id, CategoriesRequestDto dto);
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetAllCategoriesAsync();
    Task DeleteCategoryAsync(int id);
}