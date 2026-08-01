namespace WebAPI.Features.Constructor.Materials.Admin.UpdateMaterial;

public class UpdateMaterialRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

