using System.Text.Json;

namespace System;

public static class SystemExtensions
{
    public static T? Apply<T>(this T? nullable, Action<T> apply)
    {
        if (nullable is not null)
        {
            apply(nullable);
        }
        return nullable;
    }

    public static TParser? Parse<TParser>(this string? input)
        where TParser : IParsable<TParser>
    {
        if (input is null)
        {
            return default!;
        }
        _ = TParser.TryParse(input, null,  out var result);
        return result;
    }

    public static string ToJoinString<T>(this IEnumerable<T> lists, string separator)
        => String.Join(separator, lists);

    public static string ToJoinString<T>(this IEnumerable<T> lists, char separator)
        => String.Join(separator, lists);

    public static string ToJson<TData>(this TData data, JsonSerializerOptions? options = null) 
        => JsonSerializer.Serialize(data, options);
    public static TData FromJson<TData>(this string input, JsonSerializerOptions? options = null)
        => JsonSerializer.Deserialize<TData>(input, options)!;
}
