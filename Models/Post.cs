using SocialMedia.DTOs;

namespace SocialMedia.Models;

public record Post{
    public long PostId { get; set; }

    public string PostType { get; set; }

    public  DateTimeOffset DateCreated { get; set; }
    public  DateTimeOffset DateUpdated { get; set; }

    public long UserId { get; set; }

    public PostDTO asDto => new PostDTO
        {
          PostId = PostId,
          PostType = PostType,
          DateCreated = DateCreated,
          DateUpdated = DateCreated
        };
}
