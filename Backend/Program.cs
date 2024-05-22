using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Hubs;
using Backend.Models;
using Backend.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


// условная бд с пользователями
var users = new List<User>
{
    new User("Nikita", "+911","12345"),
    new User("Egor", "+112","qwerty"),
};


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
 
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
 
                // если запрос направлен хабу
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                {
                    // получаем токен из строки запроса
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapHub<ChatHub>("/chatHub");

app.MapGet("/test", () =>
{
    return "hello";
});


app.MapGet("/auth", [Authorize] () =>
{
    return "You are successfully authorized!";
});


app.MapPost("/signIn",  (SignInDto signInDto) =>
{
    User? user = users.FirstOrDefault(p => p.PhoneNumber == signInDto.PhoneNumber && p.Password == signInDto.Password);
    
    if (user is null) 
        return Results.Unauthorized();
 
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.PhoneNumber) };

    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    
    var response = new
    {
        access_token = encodedJwt,
        username = user.Name
    };
 
    return Results.Json(response);
});


app.MapPost("/signUp",  (SignUpDto signUpDto) =>
{
    User? user = users.FirstOrDefault(p => p.PhoneNumber == signUpDto.PhoneNumber);
    
    if (user is not null) 
        return Results.BadRequest("Phone number already registered");
    
    users.Add(new User(signUpDto.Name, signUpDto.PhoneNumber, signUpDto.Password));
 
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, signUpDto.PhoneNumber) };

    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    
    var response = new
    {
        access_token = encodedJwt,
        username = signUpDto.Name
    };
 
    return Results.Json(response);
});


app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}