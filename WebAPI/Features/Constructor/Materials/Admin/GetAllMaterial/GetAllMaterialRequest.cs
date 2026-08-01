namespace WebAPI.Features.Constructor.Materials.Admin.GetAllMaterial;

public class GetAllMaterialRequest : PaginatedRequest
{
    public string? Search { get; set; }
}

