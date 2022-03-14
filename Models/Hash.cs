using SocialMedia.DTOs;

namespace SocialMedia.Models;

public record Hash{
    public long HashId { get; set; }
    
    public string HashName { get; set; }

    public HashDTO asDto => new HashDTO
        {
          HashId = HashId,
          HashName = HashName,
        };
}
