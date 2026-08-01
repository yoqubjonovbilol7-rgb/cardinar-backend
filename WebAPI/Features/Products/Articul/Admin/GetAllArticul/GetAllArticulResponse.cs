using System.Linq.Expressions;

namespace WebAPI.Features.Products.Articul.Admin.GetAllArticul;

public class GetAllArticulResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductTitle { get; set; } = string.Empty;
    public int CarModelId { get; set; }
    public string CarModelTitle { get; set; } = string.Empty;

    public static Expression<Func<Entities.Articul, GetAllArticulResponse>> Project =>
        x => new GetAllArticulResponse
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductTitle = x.Product.Title,
            CarModelId = x.CarModelId,
            CarModelTitle = x.CarModel.Title
        };
}

