using System.Linq.Expressions;

namespace WebAPI.Features.Common.Color.Admin.GetAllColor;

public class GetAllColorResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ColorCode { get; set; } = string.Empty;

    public static Expression<Func<Entities.Color, GetAllColorResponse>> Project =>
        x => new GetAllColorResponse
        {
            Id = x.Id,
            Title = x.Title,
            ColorCode = x.ColorCode
        };
}

