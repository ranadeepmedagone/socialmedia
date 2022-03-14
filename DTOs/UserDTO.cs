using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialMedia.DTOs;

public record UserDTO{

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("address")]
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("post")]
    [Required]

    public List<PostDTO> Post { get; set; }

    

}

public record CreateUserDTO{



    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [JsonPropertyName("mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    [Required]
    [MaxLength(255)]
    public string Email { get; set; }

    [JsonPropertyName("date_of_birth")]

    public  DateTimeOffset DateOfBirth { get; set; }


    [JsonPropertyName("address")]
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }

    // [JsonPropertyName("created_at")]
    // public DateTimeOffset CreatedAt { get; set; }
}
public record UpdateUserDTO{



    [JsonPropertyName("user_name")]
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    // [JsonPropertyName("mobile")]
    // [Required]
    // public long Mobile { get; set; }

    // [JsonPropertyName("email")]
    // [Required]
    // [MaxLength(255)]
    // public string Email { get; set; }

    [JsonPropertyName("address")]
    [Required]
    [MaxLength(200)]
    public string Address { get; set; }


}


