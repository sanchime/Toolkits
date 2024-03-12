namespace Sanchime.Common.Models;

public interface ITree<TChild>
{
    public List<TChild> Children { get; set; }
}
