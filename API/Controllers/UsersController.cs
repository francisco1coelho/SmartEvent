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

    /// <summary>
    /// Get user by ID.
    /// 
    /// This endpoint retrieves the information of a user with the specified ID.
    /// It returns a 404 Not Found response if the user does not exist.
    /// Only users with the "Admin" role are authorized to access this endpoint.
    /// </summary>
    /// 
    ///  <param name="id">The ID of the user to retrieve.</param>
    ///  <returns>Returns an IActionResult containing the user information or a 404 Not Found response.</returns>
    ///
    //[Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Get user by email.  
    /// 
    /// This endpoint retrieves the information of a user with the specified email address.
    /// It returns a 404 Not Found response if the user does not exist.
    /// Only users with the "Admin" or "Organizer" roles are authorized to access this endpoint.
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Returns an IActionResult containing the user information or a 404 Not Found response.</returns>
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Organizer")]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(email);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// Get all users.
    /// Only users with the "Admin" or "Organizer" roles are authorized to access this endpoint.
    /// </summary>
    /// <returns>Returns an IActionResult containing the list of users.</returns>
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Organizer")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return Ok(users);
    }

    /// <summary>
    /// Update user information.
    /// 
    /// This endpoint allows updating the information of a user with the specified ID.
    /// Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    ///  
    /// <param name="dto"> The updated user information in the request body.</param>
    /// <param name="userId"> The ID of the user to update.</param>
    /// <returns>Returns an IActionResult indicating the result of the operation.</returns>

    //[Authorize(Roles = "Admin")]
    [HttpPut("{userId:int}")]
    public async Task<IActionResult> Update(int userId, [FromBody] UpdateMeDto dto)
    {
        var success = await _userService.UpdateProfileAsync(userId, dto);

        return success ? NoContent() : NotFound();
    }


    /// <summary>
    /// Update the authenticated user's profile.
    /// 
    /// This endpoint allows the authenticated user to update their own profile information.
    /// The user must be authenticated to access this endpoint.
    /// </summary>
    /// <param name="dto">The updated profile information in the request body.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    //[Authorize]
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
    /// 
    /// Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>Returns an IActionResult indicating the result of the operation.</returns>
    //[Authorize(Roles = "Admin")]
    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> Delete(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null) return NotFound();
        
        await _unitOfWork.Users.DeleteAsync(user);
        return NoContent();
    }

    /// <summary>
    /// Create a new user.
    /// Only users with the "Admin" role are authorized to perform this action.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Returns the created user.</returns>
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<User> Create([FromBody] CreateUserDto dto)
    {
        var createdUser = await _userService.CreateUserAsync(dto);
        return createdUser;
    }
}