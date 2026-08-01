using System.Linq.Expressions;

namespace WebAPI.Features.Common.CarMake.Public.GetAllCarMakes;

public class GetAllCarMakesResponsePublic
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Expression<Func<Entities.CarMake, GetAllCarMakesResponsePublic>> Project = u => new GetAllCarMakesResponsePublic
    {
        Id = u.Id,
        Title = u.Title,
        CreatedAt = u.CreatedAt,
        UpdatedAt = u.UpdatedAt
    };
}