namespace WebAPI.Features.Branches.PhoneNumber.Admin.GetAllPhoneNumber;

public class GetAllPhoneNumberRequest : PaginatedRequest
{
    public string? Search { get; set; }
    public bool? IsAdmin { get; set; }
}