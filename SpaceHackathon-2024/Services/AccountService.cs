using System.Text;
using Newtonsoft.Json;
using SpaceHackathon_2024.Models.Dtos;

namespace SpaceHackathon_2024.Services;

public class AccountService
{
    private readonly HttpClient _client;

    private const string _url = "http://10.0.2.2:5100";
    
    public AccountService(HttpClient httpClient)
    {
        _client = httpClient;
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