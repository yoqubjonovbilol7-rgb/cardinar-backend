using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Cart.CartItem.Admin.GetAllCartItem;

public class GetAllCartItemEndpoint(CardinarDbContext context)
    : Endpoint<GetAllCartItemRequest, PaginatedResponse<GetAllCartItemResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/cart-items/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("CartItems"));
    }

    public override async Task<PaginatedResponse<GetAllCartItemResponse>> ExecuteAsync(GetAllCartItemRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.CartItems.AsNoTracking().Include(x => x.Product).AsQueryable();

        if (req.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == req.ProductId.Value);
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllCartItemResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllCartItemResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

