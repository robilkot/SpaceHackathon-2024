using Newtonsoft.Json;

namespace SpaceHackathon_2024.Models.Dtos;

public record AuthResponseDto
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    
    [JsonProperty("surname")]
    public string Surname { get; set; }
}