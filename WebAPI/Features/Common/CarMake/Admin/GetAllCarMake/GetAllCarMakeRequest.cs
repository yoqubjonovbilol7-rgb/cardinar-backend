namespace WebAPI.Features.Common.CarMake.Admin.GetAllCarMake;

public class GetAllCarMakeRequest : PaginatedRequest
{
    public string? Search {get; set;}
    public bool? IsActive { get; set; }
}