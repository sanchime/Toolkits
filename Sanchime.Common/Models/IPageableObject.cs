namespace Sanchime.Common.Models;
public interface IPageableObject
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}
