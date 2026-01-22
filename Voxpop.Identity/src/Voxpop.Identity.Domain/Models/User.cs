namespace Voxpop.Identity.Domain.Models;

public class User(string phoneNumber, bool phoneNumberConfirmed = false)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string PhoneNumber { get; private set; } = phoneNumber;
    public bool PhoneNumberConfirmed { get; private set; } = phoneNumberConfirmed;
}