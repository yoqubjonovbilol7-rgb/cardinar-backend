namespace WebAPI.Features.Constructor.Entities;

public class Materials : BaseEntity
{
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<Part> Parts { get; set; } = new List<Part>();
}