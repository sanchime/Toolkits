namespace Sanchime.AspNetCore;

internal readonly struct ErrorResult
{
    public int Code { get; init; }

    public string Message { get; init; }
}
