using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMedia.DTOs;

public record PostDTO{

    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_type")]
    [Required]
    public string PostType { get; set; }
    [JsonPropertyName("date_created")]
    [Required]
     public  DateTimeOffset DateCreated { get; set; }

    [JsonPropertyName("date_updated")]
    [Required]
    public  DateTimeOffset DateUpdated { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public long UserId { get; set; }

    [JsonPropertyName("hash")]
    [Required]

    public List<HashDTO> Hash { get; set; }

    [JsonPropertyName("like")]
    [Required]

    public List<LikeDTO> Like { get; set; }





    
}

public record CreatePostDTO{



    [JsonPropertyName("post_type")]
    [Required]
    public string PostType { get; set; }
    [JsonPropertyName("date_created")]
    [Required]
     public  DateTimeOffset DateCreated { get; set; }

    [JsonPropertyName("date_updated")]
    [Required]
    public  DateTimeOffset DateUpdated { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public long UserId { get; set; }
}


