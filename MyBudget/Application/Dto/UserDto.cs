using System.Text.Json.Serialization;

namespace Application.Dto;
public class UserDto : BaseDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("birthdate")]
    public DateTime Birthdate { get; set; }
    
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
