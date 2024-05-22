using Newtonsoft.Json;

namespace SpaceHackathon_2024.Models.Dtos;

public record AuthResponseDto
{
    [JsonProperty("accessToken")]
    public string? AccessToken { get; set; }
    
    [JsonProperty("refreshToken")]
    public string? RefreshToken { get; set; }
}