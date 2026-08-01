using System.Linq.Expressions;

namespace WebAPI.Features.Common.Color.Public.GetAllColor;

public class GetAllColorResponsePublic
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ColorCode { get; set; } = null!;

    public static Expression<Func<Entities.Color, GetAllColorResponsePublic>> Project = c => new GetAllColorResponsePublic()
    {
        Id = c.Id,
        Title = c.Title,
        ColorCode = c.ColorCode,
    };
}