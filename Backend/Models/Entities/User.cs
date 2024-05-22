namespace Backend.Models;

public class User
{
    public User(string name, string phoneNumber, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}