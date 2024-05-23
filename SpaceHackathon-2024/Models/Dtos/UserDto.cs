using Newtonsoft.Json;

namespace SpaceHackathon_2024.Models.Dtos;

public record UserDto
{
    [JsonProperty("name")] 
    public string Name  { get; set; }

    [JsonProperty("phoneNumber")] 
    public string PhoneNumber  { get; set; }
};
