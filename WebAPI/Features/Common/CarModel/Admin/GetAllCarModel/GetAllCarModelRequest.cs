
namespace WebAPI.Features.Common.CarModel.Admin.GetAllCarModel;

public class GetAllCarModelRequest : PaginatedRequest
{
    public string? Search { get; set; }
    public int? CarMakeId { get; set; }
}

