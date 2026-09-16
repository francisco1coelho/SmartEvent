using Microsoft.AspNetCore.Mvc;
using SmartEvent.Application.DTOs.Users;
using SmartEvent.Application.Interfaces.Services;
using System.Security.Claims;

namespace SmartEvent.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get user by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(new UsersResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Locked = user.Locked,
            CreatedAt = user.CreatedAt
        });
    }

    /// <summary>
    /// Get user by email.
    /// </summary>
    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        if (user is null)
            return NotFound();

        return Ok(new UsersResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Locked = user.Locked,
            CreatedAt = user.CreatedAt
        });
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users.Select(user => new UsersResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Locked = user.Locked,
            CreatedAt = user.CreatedAt
        }));
    }

    /// <summary>
    /// Update user information.
    /// </summary>
    [HttpPut("{userId:int}")]
    public async Task<IActionResult> Update(int userId, [FromBody] UpdateMeDto dto)
    {
        var success = await _userService.UpdateProfileAsync(userId, dto);

        return success ? NoContent() : NotFound();
    }

    /// <summary>
    /// Update the authenticated user's profile.
    /// </summary>
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

    /// <summary>
    /// Delete a user by ID.
    /// </summary>
    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> Delete(int userId)
    {
        try
        {
            await _userService.DeleteAsync(userId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<UsersResponseDto>> Create([FromBody] CreateUserDto dto)
    {
        var createdUser = await _userService.CreateUserAsync(dto);
        var response = new UsersResponseDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Email = createdUser.Email,
            Phone = createdUser.Phone,
            Role = createdUser.Role,
            Locked = createdUser.Locked,
            CreatedAt = createdUser.CreatedAt
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}