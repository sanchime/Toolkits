namespace Sanchime.Common.Models;
public interface IPageableObject
{
    public int PageIndex { get; }

    public int PageSize { get; }
}
