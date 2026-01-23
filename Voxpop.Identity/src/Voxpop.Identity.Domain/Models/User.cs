namespace Voxpop.Identity.Domain.Models;

public class User(Guid id, string phoneNumber, string? email = null)
{
    public Guid Id { get; private set; } = id;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string? Email { get; private set; } = email;
    
    public static User Create(string phoneNumber, string? email = null) => new(Guid.NewGuid(), phoneNumber, email);
}