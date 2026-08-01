using System.Linq.Expressions;

namespace WebAPI.Features.Branches.PhoneNumber.Admin.GetAllPhoneNumber;

public class GetAllPhoneNumberResponse
{
    public int Id { get; set; }
    public string Value { get; set; }

    public static Expression<Func<Entities.PhoneNumber, GetAllPhoneNumberResponse>> Project => p => new GetAllPhoneNumberResponse
        {
            Id = p.Id,
            Value = p.Value
        };
}