namespace Backend.Models;

public class User
{
    public User(string name, string surname, string phoneNumber, string password)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Password = password;
    }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}