namespace Sanchime.Common.Models;

public interface ITraceableData
{
    public long CreatedUser { get; set; }

    public string CreatedUserName { get; set; }

    public long ModifiedUser { get; set; }

    public string ModifiedUserName { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset ModifiedDate { get; set; }
}
