using System.Linq.Expressions;

namespace WebAPI.Features.Common.CarMake.Admin.GetAllCarMake;

public class GetAllCarMakesResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public static Expression<Func<Entities.CarMake, GetAllCarMakesResponse>> Project =>
        x => new GetAllCarMakesResponse
        {
            Id = x.Id,
            Title = x.Title
        };
}