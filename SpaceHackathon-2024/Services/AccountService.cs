using System.Text;
using Newtonsoft.Json;
using SpaceHackathon_2024.Models.Dtos;

namespace SpaceHackathon_2024.Services;

public class AccountService
{
    private readonly HttpClient _client;

    private const string hostAddr = "146.70.202.4";

    private const string _url = "http://10.0.2.2:5040";
    
    private const string _extendedUrl = $"http://{hostAddr}:5040";
    
    public AccountService(HttpClient httpClient)
    {
        _client = httpClient;
    }
    
    public async Task<ProfileDto> GetProfileInfo(string token)
    {
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        
        using HttpResponseMessage response = await _client.GetAsync($"{_url}/profile");

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
        
        using HttpResponseMessage response = await _client.PostAsync($"{_url}/signIn", requestContent);

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

        using HttpResponseMessage response = await _client.PostAsync($"{_url}/signUp", requestContent);

        string jsonInfo = await response.Content.ReadAsStringAsync();
        
        AuthResponseDto res = JsonConvert.DeserializeObject<AuthResponseDto>(jsonInfo);
        
        return res;
    }
}