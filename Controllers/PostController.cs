using Microsoft.AspNetCore.Mvc;

using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.DTOs;

namespace SocialMedia.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _Post;
    private readonly IHashRepository _Hash;
    private readonly ILikeRepository _Like;
    
    

    public PostController(ILogger<PostController> logger, IPostRepository Post,IHashRepository hash,ILikeRepository Like)
    {
        _logger = logger;
        _Post = Post;
        _Hash = hash;
        _Like = Like;
    
    }
    [HttpGet]
    public async Task<ActionResult<List<PostDTO>>> GetAllPosts()
    {
        var PostsList = await _Post.GetList();

        var dtoList = PostsList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{post_id}")]

    public async Task<ActionResult<PostDTO>> GetById([FromRoute] long post_id)
    {
        var Post = await _Post.GetById(post_id);
        if (Post is null)
            return NotFound("No Post found with given employee number");
        var dto = Post.asDto;
        dto.Hash = (await _Hash.GetList(post_id)).Select(x => x.asDto).ToList();
        dto.Like = (await _Like.GetListOfLikes(post_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO Data)
    {
        var toCreatePost = new Post
        {
            PostType = Data.PostType.Trim(),
            DateCreated = Data.DateCreated,
            DateUpdated = Data.DateUpdated,
            UserId = Data.UserId,
        };
        var createdPost = await _Post.Create(toCreatePost);

        return StatusCode(StatusCodes.Status201Created, createdPost.asDto);
    }

    // [HttpPut("{Post_id}")]
    // public async Task<ActionResult> UpdatePost([FromRoute] long Post_id,
    // [FromBody] UpdatePostDTO Data)
    // {
    //     var existing = await _Post.GetById(Post_id);
    //     if (existing is null)
    //         return NotFound("No Post found with given id");

    //     var toUpdatePost = existing with
    //     {
    //         // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
    //         PostName = Data.PostName?.Trim() ?? existing.PostName,
    //         // Mobile = Data.Mobile,
    //         Address = Data.Address?.Trim()?.ToLower() ?? existing.Address,
        
    //     };

    //     var didUpdate = await _Post.Update(toUpdatePost);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
    //     return NoContent();
    // }

    [HttpDelete("{post_id}")]
    public async Task<ActionResult> DeletePost([FromRoute] long post_id)
    {
        var existing = await _Post.GetById(post_id);
        if (existing is null)
            return NotFound("No Post found with given id");
        await _Post.Delete(post_id);
        return NoContent();
    }



}
