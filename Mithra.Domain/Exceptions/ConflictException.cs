namespace Mithra.Domain.Exceptions;

// TODO: why sealed?
public sealed class ConflictException(string message) : Exception(message);