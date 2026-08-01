using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Public.GetOneCategory;

public class GetOneCategoriesEndpointPublic(CardinarDbContext context)
    : Endpoint<GetOneCategoriesRequestPublic, GetOneCategoriesResponsePublic>
{
    public override void Configure()
    {
        Get("v1/public/categories/get-one-category/{Id:int}");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("ProductCategories"));
    }

    public override async Task<GetOneCategoriesResponsePublic> ExecuteAsync(GetOneCategoriesRequestPublic req,
        CancellationToken ct)
    {
        var categories = await context.ProductCategories.SingleOrDefaultAsync(c => c.Id == req.Id);

        if (categories == null)
            throw new Exception("Categories with given id does not exists.");

        return new GetOneCategoriesResponsePublic
        {
            Id = categories.Id,
            Title = categories.Title
        };
    }
}