namespace WebAPI.Features.Branches.PhoneNumber.Public.GetAllPhoneNumbers;

public class GetAllPhoneNumberRequestPublic : PaginatedRequest
{
    public string? Search { get; set; }
}