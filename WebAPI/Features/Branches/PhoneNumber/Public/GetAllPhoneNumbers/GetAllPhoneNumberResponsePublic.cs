using System.Linq.Expressions;

namespace WebAPI.Features.Branches.PhoneNumber.Public.GetAllPhoneNumbers;

public class GetAllPhoneNumberResponsePublic
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static Expression<Func<Entities.PhoneNumber, GetAllPhoneNumberResponsePublic>> Project = p => new GetAllPhoneNumberResponsePublic
    {
        Id = p.Id,
        Value = p.Value,
        CreatedAt = p.CreatedAt,
        UpdatedAt = p.UpdatedAt
    };
}