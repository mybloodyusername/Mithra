using System.Text.Json.Serialization;

namespace Mithra.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRoles
{
    Admin,
    User,
}