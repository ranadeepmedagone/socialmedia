using Microsoft.AspNetCore.Mvc;

using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.DTOs;

namespace SocialMedia.Controllers;

[ApiController]
[Route("api/like")]
public class LikeController : ControllerBase
{
    private readonly ILogger<LikeController> _logger;
    private readonly ILikeRepository _Like;
    // private readonly IUserRepository _User;
    
    

    public LikeController(ILogger<LikeController> logger, ILikeRepository Like )
    {
        _logger = logger;
        _Like = Like;
    
    }
    
    // [HttpGet("{like_id}")]

    // public async Task<ActionResult<LikeDTO>> GetById([FromRoute] long like_id)
    // {
    //     var Like = await _Like.GetById(like_id);
    //     if (Like is null)
    //         return NotFound("No Like found with given employee number");
    //         var dto = Like.asDto;
    //         dto.User = (await _User.GetList(like_id)).Select(x => x.Dto).ToList();

    //     return Ok(dto);
    // }

    [HttpPost]

    public async Task<ActionResult<LikeDTO>> CreateLike([FromBody] CreateLikeDTO Data)
    {
        var toCreateLike = new Like
        {
        
            DateCreated = Data.DateCreated,
            UserId = Data.UserId,
            PostId = Data.PostId,
        };
        var createdLike = await _Like.Create(toCreateLike);

        return StatusCode(StatusCodes.Status201Created, createdLike.asDto);
    }



    [HttpDelete("{like_id}")]
    public async Task<ActionResult> DeleteLike([FromRoute] long like_id)
    {
        var existing = await _Like.GetById(like_id);
        if (existing is null)
            return NotFound("No Like found with given id");
        await _Like.Delete(like_id);
        return NoContent();
    }



}
