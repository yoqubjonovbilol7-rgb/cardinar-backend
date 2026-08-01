namespace WebAPI.Features.Constructor.Materials.Admin.CreateMaterial;

public class CreateMaterialResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

