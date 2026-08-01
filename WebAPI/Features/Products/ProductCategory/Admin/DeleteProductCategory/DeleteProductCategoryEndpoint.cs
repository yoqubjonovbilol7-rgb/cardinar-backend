using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Admin.DeleteProductCategory;

public class DeleteProductCategoryEndpoint(CardinarDbContext context)
    : Endpoint<DeleteProductCategoryRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/product-categories/delete/{id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(options => options.WithTags("ProductCategories"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteProductCategoryRequest req, CancellationToken ct)
    {
        var category = await context.ProductCategories
            .SingleOrDefaultAsync(c => c.Id == req.Id, ct);

        if (category == null)
        {
            throw new Exception($"Product category with id {req.Id} does not exist.");
        }

        context.ProductCategories.Remove(category);
        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}