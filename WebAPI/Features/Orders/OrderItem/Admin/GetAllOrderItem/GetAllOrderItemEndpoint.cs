using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Orders.OrderItem.Admin.GetAllOrderItem;

public class GetAllOrderItemEndpoint(CardinarDbContext context)
    : Endpoint<GetAllOrderItemRequest, PaginatedResponse<GetAllOrderItemResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/order-items/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("OrderItems"));
    }

    public override async Task<PaginatedResponse<GetAllOrderItemResponse>> ExecuteAsync(GetAllOrderItemRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.OrderItems.AsNoTracking().Include(x => x.Product).AsQueryable();

        if (req.OrderId.HasValue)
        {
            query = query.Where(x => x.OrderId == req.OrderId.Value);
        }

        if (req.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == req.ProductId.Value);
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllOrderItemResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllOrderItemResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

