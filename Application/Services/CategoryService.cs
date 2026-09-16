// Application/Services/CategoryService.cs
using SmartEvent.Application.DTOs.Categories;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Interfaces.Services;
using SmartEvent.Domain.Entities;

namespace SmartEvent.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Category> CreateCategoryAsync(CategoriesRequestDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _unitOfWork.Categories.Add(category);
        await _unitOfWork.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(int id, CategoriesRequestDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new ArgumentException($"Category with ID {id} not found.");
        }

        category.Name = dto.Name;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _unitOfWork.Categories.GetByIdAsync(id);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _unitOfWork.Categories.GetAllAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category == null)
        {
            throw new ArgumentException($"Category with ID {id} not found.");
        }

        _unitOfWork.Categories.Remove(category);
        await _unitOfWork.SaveChangesAsync();
    }
}