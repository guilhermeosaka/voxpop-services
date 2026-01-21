namespace Voxpop.Identity.Domain.Models;

public class User(string phoneNumber, bool phoneNumberConfirmed = false)
{
    public string PhoneNumber { get; private set; } = phoneNumber;
    public bool PhoneNumberConfirmed { get; private set; } = phoneNumberConfirmed;
    public string? VerificationCode { get; private set; }
    public DateTime? VerificationCodeExpiresAt { get; private set; }
    
    public void GenerateVerificationCode(TimeSpan expiresIn)
    {
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        var number = BitConverter.ToUInt32(bytes, 0) % 1000000;
        VerificationCode = number.ToString("D6");
        VerificationCodeExpiresAt = DateTime.UtcNow.Add(expiresIn);
    }
}