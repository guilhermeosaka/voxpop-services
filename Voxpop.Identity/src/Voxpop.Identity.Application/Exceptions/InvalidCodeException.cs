namespace Voxpop.Identity.Application.Exceptions;

public class InvalidCodeException(string code) : Exception($"Code '{code}' is invalid.");