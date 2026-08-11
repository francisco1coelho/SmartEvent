// Application/Services/CategoryService.cs
using SmartEvent.Application.DTOs.CategoriesDto;
using SmartEvent.Application.Interfaces;

namespace SmartEvent.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    
}