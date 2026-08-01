using Microsoft.EntityFrameworkCore;
using WebApi.Features.Products.ProductCategories.Admin.GetOneCategory;

namespace WebAPI.Features.Products.ProductCategory.Admin.GetOneCategory;

public class GetOneCategoriesEndpoint(CardinarDbContext context) : Endpoint<GetOneCategoriesRequest, GetOneCategoriesResponse>
{
    public override void Configure()
    {
        Get("v1/admin/categories/get-one-category/{Id:int}");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("ProductCategories"));
    }

    public override async Task<GetOneCategoriesResponse> ExecuteAsync(GetOneCategoriesRequest req, CancellationToken ct)
    {
        var categories = await context.ProductCategories.SingleOrDefaultAsync(c => c.Id == req.Id);

        if (categories == null)
            throw new Exception("Categories with given id does not exists.");

        return new GetOneCategoriesResponse
        {
            Id = categories.Id,
            Title = categories.Title
        };
    }
}