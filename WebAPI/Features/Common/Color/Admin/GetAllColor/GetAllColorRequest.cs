namespace WebAPI.Features.Common.Color.Admin.GetAllColor;

public class GetAllColorRequest : PaginatedRequest
{
    public string? Search { get; set; }
}

