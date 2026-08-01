namespace WebAPI.Features.Common.CarModel.Public.GetAllCarModels;

public class GetAllCarModelsRequestPublic : PaginatedRequest
{
    public string? Search { get; set; }
}