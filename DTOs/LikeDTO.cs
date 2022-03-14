using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMedia.DTOs;

public record LikeDTO{

    [JsonPropertyName("like_id")]
    [Required]
    public long LikeId { get; set; }

    [JsonPropertyName("date_created")]
    [Required]
    public  DateTimeOffset DateCreated { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public long UserId { get; set; }

    [JsonPropertyName("post_id")]
    [Required]
    public long PostId { get; set; }

    
}

public record CreateLikeDTO{

    

    [JsonPropertyName("date_created")]
    [Required]
    public  DateTimeOffset DateCreated { get; set; }

    [JsonPropertyName("user_id")]
    [Required]
    public long UserId { get; set; }

    [JsonPropertyName("post_id")]
    [Required]
    public long PostId { get; set; }

    
}

