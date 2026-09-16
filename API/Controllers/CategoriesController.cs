using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Categories;
using SmartEvent.Application.Interfaces.Services;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Gets a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category is null)
            return NotFound();

        return Ok(category);
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Deletes a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Organizer")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Updates a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="category"></param>
    /// <returns></returns>
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Organizer")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriesRequestDto category)
    {
        try
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
            return Ok(updatedCategory);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}