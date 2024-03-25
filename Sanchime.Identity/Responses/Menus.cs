﻿using Sanchime.Identity.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanchime.Identity.Responses;

public class MenuTreeResponse : ITree<MenuTreeResponse>
{
    public long Id { get; set; }

    public required string Code { get; set; }

    public required string Name { get; set; }

    public MenuType Type { get; set; }

    public string? Icon { get; set; }

    public int Order { get; set; }

    public string? Route { get; set; }

    public string? Path { get; set; }

    public string? Description { get; set; }

    public long? ParentId { get; set; }

    [NotMapped]
    [AdaptIgnore]
    public List<MenuTreeResponse> Children { get; set; } = [];
}