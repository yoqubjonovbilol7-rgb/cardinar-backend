using System.Linq.Expressions;

namespace WebAPI.Features.Constructor.Materials.Admin.GetAllMaterial;

public class GetAllMaterialResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static Expression<Func<Entities.Materials, GetAllMaterialResponse>> Project =>
        x => new GetAllMaterialResponse
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description
        };
}

