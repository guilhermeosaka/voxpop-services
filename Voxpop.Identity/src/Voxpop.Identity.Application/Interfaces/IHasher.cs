namespace Voxpop.Identity.Application.Interfaces;

public interface IHasher
{
    public string Hash(string plainText);

    public bool Verify(string hashed, string plainText);
}