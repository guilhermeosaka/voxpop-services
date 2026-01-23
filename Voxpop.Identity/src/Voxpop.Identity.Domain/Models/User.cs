namespace Voxpop.Identity.Domain.Models;

public class User(string phoneNumber, string? email = null, bool phoneNumberConfirmed = false)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string? Email { get; private set; } = email;
    public bool PhoneNumberConfirmed { get; private set; } = phoneNumberConfirmed;
}