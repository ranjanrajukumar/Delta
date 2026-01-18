using System.Text.Json;

namespace Delta.Shared.Helpers;

public static class JsonHelper
{
    public static string Serialize<T>(T data)
        => JsonSerializer.Serialize(data);
}
