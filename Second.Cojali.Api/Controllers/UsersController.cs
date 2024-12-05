using AutoMapper;
using Second.Cojali.Api.Contracts.Request;
using Second.Cojali.Api.Contracts.Dtos.Request;
using Second.Cojali.Api.Contracts.Dtos.Response;
using Second.Cojali.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Second.Cojali.Api.Controllers;

/// <summary>
/// Manages user-related operations such as retrieval, creation, and updates.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all registered users.
    /// </summary>
    /// <remarks>
    /// This endpoint retrieves a list of all users currently in the system.
    /// Use this to fetch user data for administrative or reporting purposes.
    /// </remarks>
    /// <returns>A list of users with their details.</returns>
    /// <response code="200">Successfully retrieved the list of users.</response>
    /// <response code="500">An error occurred while processing the request.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), 200)] // OK
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllUsersAsync();
        var response = _mapper.Map<IEnumerable<UserResponseDto>>(users);
        return Ok(response);
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <remarks>
    /// This endpoint creates a new user with the provided details. 
    /// Ensure that the email is unique to avoid duplication errors.
    /// </remarks>
    /// <param name="userDto">An object containing the user's name and email.</param>
    /// <returns>The details of the newly created user.</returns>
    /// <response code="201">User was successfully created.</response>
    /// <response code="400">Invalid input or missing required fields.</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDto), 201)] // Created
    [ProducesResponseType(400)] // Bad Request
    public async Task<IActionResult> AddUserAsync([FromBody] AddRequestDto userDto)
    {
        var user = await _userService.CreateUserAsync(userDto.Name, userDto.Email);
        var response = _mapper.Map<UserResponseDto>(user);
        return Created("", response);
    }

    /// <summary>
    /// Updates an existing user's details.
    /// </summary>
    /// <remarks>
    /// This endpoint allows updating the name and email of an existing user. 
    /// Ensure the user ID provided exists in the system; otherwise, a 404 response will be returned. 
    /// The input data must be valid and conform to any constraints defined in the request model.
    /// </remarks>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="userDto">The updated user details (name and email).</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">The user was successfully updated.</response>
    /// <response code="400">The request data is invalid (e.g., missing fields, validation errors).</response>
    /// <response code="404">No user exists with the provided ID.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)] // No Content
    [ProducesResponseType(400)] // Bad Request
    [ProducesResponseType(404)] // Not Found
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateRequestDto userDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _userService.UserExistsAsync(id))
            return NotFound();

        await _userService.UpdateUserAsync(id, userDto.Name, userDto.Email);
        return NoContent();
    }
}
