using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Application.Interfaces;
using SmartEvent.Application.Services;
using SmartEvent.Domain.Entities;
using System.Security.Claims;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public UsersController(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    /**
     * Get user by ID.
     * 
     * This endpoint retrieves the information of a user with the specified ID.
     * It returns a 404 Not Found response if the user does not exist.
     *
     * @param id The ID of the user to retrieve.
     * @return An IActionResult containing the user information or a 404 Not Found response.
     */
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    /**
     * Update user information.
     * 
     * This endpoint allows updating the information of a user with the specified ID.
     * Only users with the "Admin" role are authorized to perform this action.
     *
     * @param id The ID of the user to update.
     * @param user The updated user information in the request body.
     * @return An IActionResult indicating the result of the operation.
     */

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int userId, [FromBody] UpdateMeDto dto)
    {
        var success = await _userService.UpdateProfileAsync(userId, dto);

        return success ? NoContent() : NotFound();
    }

    /**
     * Update the authenticated user's profile.
     * 
     * This endpoint allows the authenticated user to update their own profile information.
     * The user must be authenticated to access this endpoint.
     *
     * @param dto The updated profile information in the request body.
     * @return An IActionResult indicating the result of the operation.
     */

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateMeDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized();

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var success = await _userService.UpdateProfileAsync(userId, dto);

        return success ? NoContent() : NotFound();
    }

    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Organizer")]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);

        if (user is null)
            return NotFound();

        return Ok(user);
    }
}