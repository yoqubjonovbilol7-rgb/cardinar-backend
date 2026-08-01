namespace WebAPI.Features.Common.Color.Public.GetAllColor;

public class GetAllColorRequestPublic : PaginatedRequest
{
    public string? Search { get; set; }
}