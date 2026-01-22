namespace Voxpop.Identity.Application.Exceptions;

public class UserAlreadyExistsException(string phoneNumber)
    : Exception($"User with phone number '{phoneNumber}' already exists.");