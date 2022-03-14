using Microsoft.AspNetCore.Mvc;

using SocialMedia.Models;
using SocialMedia.Repositories;
using SocialMedia.DTOs;

namespace SocialMedia.Controllers;

[ApiController]
[Route("api/hash")]
public class HashController : ControllerBase
{
    private readonly ILogger<HashController> _logger;
    private readonly IHashRepository _Hash;
    private readonly IPostRepository _Post;
    public HashController(ILogger<HashController> logger, IHashRepository Hash,IPostRepository Post )
    {
        _logger = logger;
        _Hash = Hash;
        _Post = Post;
    
    }

    [HttpGet("{hash_id}")]

    public async Task<ActionResult<HashDTO>> GetById([FromRoute] long hash_id)
    {
        var hash = await _Hash.GetById(hash_id);
        if (hash is null)
            return NotFound("No Hash found with given employee number");
            var dto = hash.asDto;
        dto.Post = (await _Post.GetListOfPost(hash_id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<HashDTO>> CreateHash([FromBody] CreateHashDTO Data)
    {
        var toCreateHash = new Hash
        {
        
            HashName =Data.HashName,
        };
        var createdHash = await _Hash.Create(toCreateHash);

        return StatusCode(StatusCodes.Status201Created, createdHash.asDto);
    }

    [HttpDelete("{Hash_id}")]
    public async Task<ActionResult> DeleteHash([FromRoute] long Hash_id)
    {
        var existing = await _Hash.GetById(Hash_id);
        if (existing is null)
            return NotFound("No Hash found with given id");
        await _Hash.Delete(Hash_id);
        return NoContent();
    }



}
