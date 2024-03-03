namespace Sanchime.Common.Models;

public interface ICurrentUserContext
{
    public bool IsLogined { get; }

    public string Account { get; }

    public long UserId { get; }

    public string UserName { get; }

    public string? Token { get; }
}
