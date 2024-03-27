namespace Sanchime.Common.Models;

public interface ITree<TChild>
{
    public IList<TChild> Children { get; set; }
}
