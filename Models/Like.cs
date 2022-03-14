using SocialMedia.DTOs;

namespace SocialMedia.Models;

public record Like{
    public long LikeId { get; set; }
    
    public  DateTimeOffset DateCreated { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }

    public LikeDTO asDto => new LikeDTO
        {
          LikeId = LikeId,
          
          DateCreated = DateCreated,
          UserId = UserId,
          PostId = PostId
        };
}
