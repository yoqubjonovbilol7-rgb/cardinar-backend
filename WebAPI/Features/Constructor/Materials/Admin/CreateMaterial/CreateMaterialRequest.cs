namespace WebAPI.Features.Constructor.Materials.Admin.CreateMaterial;

public class CreateMaterialRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

