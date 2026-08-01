using WebAPI.Core.Enums;
using WebAPI.Features.Common.Entities;

namespace WebAPI.Features.Constructor.Entities;

public class Part : BaseEntity
{
    public Category Category { get; set; }

    public PartType PartType { get; set; }

    public string? Title { get; set; }

    public int MaterialId { get; set; }

    public int ColorId { get; set; }

    public string Image { get; set; } = null!;

    public Materials Material { get; set; } = null!;

    public Color Color { get; set; } = null!;
}