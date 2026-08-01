using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Orders.OrderItem.Admin.UpdateOrderItem;

public class UpdateOrderItemEndpoint(CardinarDbContext context)
    : Endpoint<UpdateOrderItemRequest, UpdateOrderItemResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/order-items/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("OrderItems"));
    }

    public override async Task<UpdateOrderItemResponse> ExecuteAsync(UpdateOrderItemRequest req, CancellationToken ct)
    {
        var orderItem = await context.OrderItems.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (orderItem is null)
            throw new Exception("Order item not found.");

        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);

        if (order is null)
            throw new Exception("Order not found.");

        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.ArticulId, ct);

        if (articul is null)
            throw new Exception("Articul not found.");

        orderItem.OrderId = req.OrderId;
        orderItem.ProductId = req.ProductId;
        orderItem.ArticulId = req.ArticulId;
        orderItem.Quantity = req.Quantity;

        await context.SaveChangesAsync(ct);

        return new UpdateOrderItemResponse
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            ArticulId = orderItem.ArticulId,
            Quantity = orderItem.Quantity
        };
    }
}

