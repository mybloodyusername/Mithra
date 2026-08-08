namespace Mithra.Domain.Exceptions;

// TODO: why sealed?
public sealed class NotFoundException(string message) : Exception(message);