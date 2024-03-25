using System.ComponentModel.DataAnnotations.Schema;

namespace Sanchime.Identity.Responses;

public record PermissionResponse(long Id, string Code, string Name, string? Descirption, bool IsEnabled);

public class PermissionTreeResponse : ITree<PermissionTreeResponse>
{
    public long Id { get; set; }

    public required string Code { get; set; }

    public string? Description { get; set; }

    public bool IsEnable { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    [AdaptIgnore]
    public List<PermissionTreeResponse> Children { get; set; } = [];
}
