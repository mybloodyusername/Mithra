namespace Mithra.Application.Exceptions;

// TODO: why sealed?
public sealed class NotFoundException(string message) : Exception(message);