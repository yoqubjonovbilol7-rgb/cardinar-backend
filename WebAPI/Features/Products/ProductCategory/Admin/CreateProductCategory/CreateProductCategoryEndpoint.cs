using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Products.ProductCategory.Admin.CreateProductCategory;

public class CreateCategoryEndpoint(CardinarDbContext context)
    : Endpoint<CreateProductCategoryRequest, CreateProductCategoryResponse>
{
    public override void Configure()
    {
        Post("v1/admin/product-categories/create");
        Policies("Admin");
        Tags("Admin");
        Options(x => x.WithTags("ProductCategories"));
    }

    public override async Task<CreateProductCategoryResponse> ExecuteAsync(CreateProductCategoryRequest req, CancellationToken ct)
    {
        var exists = await context.ProductCategories
            .AnyAsync(x => x.Title == req.Title, ct);

        if (exists)
            throw new Exception("Category already exists.");

        var category = new Entities.ProductCategory
        {
            Title = req.Title
        };

        context.ProductCategories.Add(category);
        await context.SaveChangesAsync(ct);

        return new CreateProductCategoryResponse
        {
            Id = category.Id,
            Title = category.Title
        };
    }
}