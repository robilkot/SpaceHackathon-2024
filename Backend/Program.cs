using Backend.Hubs;
using Backend.Models;
using Backend.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

var users = new List<User>
{
    new User("Nikita", "Kharashun" ,"+911","123"),
    new User("Nikita", "Kalabin" ,"+112","123"),
    new User("Тимур",  "Робилко","+234","123"),
    new User("Egor",  "Gokov","+119","123")
};

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5040");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

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
                
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
                {
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

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapHub<ChatHub>("/chatHub");

app.MapGet("/test", () =>
{
    var host = Dns.GetHostEntry(Dns.GetHostName());
    return host.ToString();
});

app.MapGet("/auth", [Authorize] () =>
{
    return "You are successfully authorized!";
});

app.MapGet("/profile", [Authorize] (HttpContext context) =>
{
    string? phoneNumber = context.User.FindFirst(ClaimTypes.Name)?.Value;
    
    if (string.IsNullOrEmpty(phoneNumber))
        return Results.Unauthorized();
    
    var user = users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);
    
    if (user == null)
        return Results.NotFound();
    
    return Results.Ok(new
    {
        name = user.Name,
        phoneNumber = user.PhoneNumber
    });
});


app.MapGet("/search", (string name) =>
{
    var matchingUsers = users.Where(u => u.Name.ToLower().Contains(name.ToLower())).ToList();

    if (matchingUsers.Count == 0)
        return Results.NotFound("Пользователи с таким именем не найдены.");

    return Results.Ok(matchingUsers.Select(u => new { u.Name, u.PhoneNumber }));
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
        surname = user.Surname
    };
 
    return Results.Json(response);
});

/*app.MapPost("/signUp",  (SignUpDto signUpDto) =>
{
    User? user = users.FirstOrDefault(p => p.PhoneNumber == signUpDto.PhoneNumber);
    
    if (user is not null) 
        return Results.BadRequest("Phone number already registered");
    
    users.Add(new User(signUpDto, signUpDto.PhoneNumber, signUpDto.Password));
 
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
});*/

app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretsecretsecretkey!123"; 
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}