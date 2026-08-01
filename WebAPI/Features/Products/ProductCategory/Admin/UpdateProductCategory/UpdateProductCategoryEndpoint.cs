using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Admin.UpdateProductCategory;

public class UpdateCategoryEndpoint(CardinarDbContext context)
    : Endpoint<UpdateProductCategoryRequest, UpdateProductCategoryResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/product-categories/update");
        Policies("Admin");
        Tags("Admin");
        Options(x => x.WithTags("ProductCategories"));
    }

    public override async Task<UpdateProductCategoryResponse> ExecuteAsync(UpdateProductCategoryRequest req, CancellationToken ct)
    {
        var category = await context.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (category == null)
            throw new Exception("Category not found.");

        var exists = await context.ProductCategories
            .AnyAsync(x => x.Title == req.Title && x.Id != req.Id, ct);

        if (exists)
            throw new Exception("Category already exists.");

        category.Title = req.Title;

        await context.SaveChangesAsync(ct);

        return new UpdateProductCategoryResponse
        {
            Id = category.Id,
            Title = category.Title
        };
    }
}