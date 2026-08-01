using System.Linq.Expressions;

namespace WebAPI.Features.Common.CarModel.Admin.GetAllCarModel;

public class GetAllCarModelsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int CarMakeId { get; set; }
    public string CarMakeTitle { get; set; } = string.Empty;

    public static Expression<Func<Entities.CarModel, GetAllCarModelsResponse>> Project =>
        x => new GetAllCarModelsResponse
        {
            Id = x.Id,
            Title = x.Title,
            CarMakeId = x.CarMakeId,
            CarMakeTitle = x.CarMake.Title
        };
}

