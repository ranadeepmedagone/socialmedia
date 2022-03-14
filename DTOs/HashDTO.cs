using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMedia.DTOs;

public record HashDTO{

    [JsonPropertyName("hash_id")]
    [Required]
    public long HashId { get; set; }

    [JsonPropertyName("hash_name")]
    [Required]
    [MaxLength(255)]
    public string HashName { get; set; }

    [JsonPropertyName("post")]
    [Required]

    public List<PostDTO> Post { get; set; }


    
}

public record CreateHashDTO{

    
    [JsonPropertyName("hash_name")]
    [Required]
    [MaxLength(255)]
    public string HashName { get; set; }

    
    

    
}

