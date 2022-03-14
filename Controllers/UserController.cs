using Microsoft.AspNetCore.Mvc;

using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.DTOs;

namespace SocialMedia.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _User;
    private readonly IPostRepository _Post;
    
    

    public UserController(ILogger<UserController> logger, IUserRepository User,IPostRepository Post )
    {
        _logger = logger;
        _User = User;
        _Post  = Post;
    
    }
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
    {
        var UsersList = await _User.GetList();

        var dtoList = UsersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{user_id}")]

    public async Task<ActionResult<UserDTO>> GetById([FromRoute] long user_id)
    {
        var User = await _User.GetById(user_id);
        if (User is null)
            return NotFound("No User found with given employee number");
            var dto = User.asDto;
             dto.Post = (await _Post.GetList(user_id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO Data)
    {
        var toCreateUser = new User
        {
            UserName = Data.UserName.Trim(),
            Mobile = Data.Mobile,
            Email = Data.Email.Trim().ToLower(),
            Address = Data.Address,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            // CreatedAt = Data.CreatedAt,
        };
        var createdUser = await _User.Create(toCreateUser);

        return StatusCode(StatusCodes.Status201Created, createdUser.asDto);
    }

    [HttpPut("{user_id}")]
    public async Task<ActionResult> UpdateUser([FromRoute] long user_id,
    [FromBody] UpdateUserDTO Data)
    {
        var existing = await _User.GetById(user_id);
        if (existing is null)
            return NotFound("No User found with given id");

        var toUpdateUser = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            UserName = Data.UserName?.Trim() ?? existing.UserName,
            // Mobile = Data.Mobile,
            Address = Data.Address?.Trim()?.ToLower() ?? existing.Address,
        
        };

        var didUpdate = await _User.Update(toUpdateUser);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    // [HttpDelete("{user_id}")]
    // public async Task<ActionResult> DeleteUser([FromRoute] long user_id)
    // {
    //     var existing = await _User.GetById(user_id);
    //     if (existing is null)
    //         return NotFound("No User found with given id");
    //     await _User.Delete(user_id);
    //     return NoContent();
    // }



}
