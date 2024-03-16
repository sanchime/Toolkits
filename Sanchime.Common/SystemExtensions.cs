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

    public static TParsableObject? Parse<TParsableObject>(this string? input)
        where TParsableObject : IParsable<TParsableObject>
    {
        if (input is null)
        {
            return default!;
        }
        _ = TParsableObject.TryParse(input, null,  out var result);
        return result;
    }

    public static string ToJoinString<T>(this IEnumerable<T> lists, string separator)
        => String.Join(separator, lists);

    public static string ToJoinString<T, R>(this IEnumerable<T> lists, Func<T, R> mapper, string separator)
        => String.Join(separator, lists.Select(mapper));

    public static string ToJoinString<T>(this IEnumerable<T> lists, char separator)
        => String.Join(separator, lists);

    public static string ToJoinString<T, R>(this IEnumerable<T> lists, Func<T, R> mapper, char separator)
       => String.Join(separator, lists.Select(mapper));

    public static string ToJson<TData>(this TData data, JsonSerializerOptions? options = null) 
        => JsonSerializer.Serialize(data, options);
    public static TData FromJson<TData>(this string input, JsonSerializerOptions? options = null)
        => JsonSerializer.Deserialize<TData>(input, options)!;

    public static bool IsNullOrEmpty(this string input) => String.IsNullOrEmpty(input);

    public static bool IsNullOrWhiteSpace(this string input) => String.IsNullOrWhiteSpace(input);
}
