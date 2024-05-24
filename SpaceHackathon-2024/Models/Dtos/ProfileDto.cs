using Newtonsoft.Json;

namespace SpaceHackathon_2024.Models.Dtos;

public record ProfileDto
{
    [JsonProperty("name")] 
    public string Name { get; set; }
    
    [JsonProperty("phoneNumber")] 
    public string phoneNumber { get; set; }
};