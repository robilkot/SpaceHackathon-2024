using System.Text;
using Newtonsoft.Json;
using SpaceHackathon_2024.Models.Dtos;

namespace SpaceHackathon_2024.Services;

public class AccountService
{
    private readonly HttpClient _client;

    private const string hostAddr = "192.168.191.95";

    private const string _url = "http://10.0.2.2:10010";
    
    private const string _extendedUrl = $"http://{hostAddr}:10010";
    
    public AccountService(HttpClient httpClient)
    {
        _client = httpClient;
    }
    
    public async Task<ProfileDto> GetProfileInfo(string token)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        
        using HttpResponseMessage response = await _client.GetAsync($"{_extendedUrl}/profile");

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        ProfileDto profileDto = JsonConvert.DeserializeObject<ProfileDto>(jsonInfo);
        
        return profileDto;
    }
    
    public async Task<AuthResponseDto> SignIn(string phoneNumber, string password)
    {
        var authData = new
        {
            phoneNumber,
            password
        };
            
        string jsonContent = JsonConvert.SerializeObject(authData);

        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        
        using HttpResponseMessage response = await _client.PostAsync($"{_extendedUrl}/signIn", requestContent);

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        AuthResponseDto res = JsonConvert.DeserializeObject<AuthResponseDto>(jsonInfo);
        
        return res;
    }

    public async Task<AuthResponseDto> SignUp(string phoneNumber, string password, string name)
    {
        var authData = new
        {
            name,
            phoneNumber,
            password,
        };
            
        string jsonContent = JsonConvert.SerializeObject(authData);

        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await _client.PostAsync($"{_extendedUrl}/signUp", requestContent);

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        AuthResponseDto res = JsonConvert.DeserializeObject<AuthResponseDto>(jsonInfo);
        
        return res;
    }
    
    public async Task<List<UserDto>> SearchColleage(string name)
    {
        using HttpResponseMessage response = await _client.GetAsync($"{_extendedUrl}/search?name={name}");

        List<UserDto> res = new List<UserDto>();
        
        if (response is not null)
        {
            string jsonInfo = await response.Content.ReadAsStringAsync();

            res = JsonConvert.DeserializeObject<List<UserDto>>(jsonInfo);
        }
        
        return res;
    }
}