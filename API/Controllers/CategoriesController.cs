using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.Interfaces;
using SmartEvent.Domain.Entities;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoriesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Organizer")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category is null)
            return NotFound();

        _unitOfWork.Categories.Remove(category);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Organizer")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Category category)
    {
        var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id);

        if (existingCategory is null)
            return NotFound();

        existingCategory.Name = category.Name;

        _unitOfWork.Categories.Update(existingCategory);
        await _unitOfWork.SaveChangesAsync();

        return Ok(existingCategory);
    }
}