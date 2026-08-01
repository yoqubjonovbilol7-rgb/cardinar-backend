using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Orders.OrderItem.Admin.CreateOrderItem;

public class CreateOrderItemEndpoint(CardinarDbContext context)
    : Endpoint<CreateOrderItemRequest, CreateOrderItemResponse>
{
    public override void Configure()
    {
        Post("v1/admin/order-items/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("OrderItems"));
    }

    public override async Task<CreateOrderItemResponse> ExecuteAsync(CreateOrderItemRequest req, CancellationToken ct)
    {
        var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);

        if (order is null)
            throw new Exception("Order not found.");

        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.ArticulId, ct);

        if (articul is null)
            throw new Exception("Articul not found.");

        var orderItem = new Entities.OrderItem
        {
            OrderId = req.OrderId,
            ProductId = req.ProductId,
            ArticulId = req.ArticulId,
            Quantity = req.Quantity
        };

        context.OrderItems.Add(orderItem);
        await context.SaveChangesAsync(ct);

        return new CreateOrderItemResponse
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            ArticulId = orderItem.ArticulId,
            Quantity = orderItem.Quantity
        };
    }
}

