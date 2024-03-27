namespace Sanchime.Identity.Entities;

public class Resource : IdentityEntity
{

    public required string Name { get; set; }

    public required string Endpoint { get; set; }

    public required ResourceMethod Method { get; set; }

    public string? Description { get; set; }
}

