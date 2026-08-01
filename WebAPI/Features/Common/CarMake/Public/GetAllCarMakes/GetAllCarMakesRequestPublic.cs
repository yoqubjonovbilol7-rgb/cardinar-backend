namespace WebApi.Features.Common.CarMakes.Public.GetAllCarMakes;

public class GetAllCarMakesRequestPublic : PaginatedRequest
{
    public string? Search { get; set; }
}