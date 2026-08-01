namespace WebAPI.Features.Products.Articul.Admin.GetAllArticul;

public class GetAllArticulRequest : PaginatedRequest
{
    public int? ProductId { get; set; }
    public int? CarModelId { get; set; }
}

