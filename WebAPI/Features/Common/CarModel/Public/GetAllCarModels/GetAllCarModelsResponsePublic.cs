using System.Linq.Expressions;

namespace WebAPI.Features.Common.CarModel.Public.GetAllCarModels;

public class GetAllCarModelsResponsePublic
{
    public int Id { get; set; }
    public int CarMakeId { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Expression<Func<Entities.CarModel, GetAllCarModelsResponsePublic>> Project = cm => new GetAllCarModelsResponsePublic()
    {
        Id = cm.Id,
        CarMakeId = cm.CarMakeId,
        Title = cm.Title,
        CreatedAt = cm.CreatedAt,
        UpdatedAt = cm.UpdatedAt,
    };
}