using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Orders.OrderItem.Admin.DeleteOrderItem;

public class DeleteOrderItemEndpoint(CardinarDbContext context)
    : Endpoint<DeleteOrderItemRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/order-items/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("OrderItems"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteOrderItemRequest req, CancellationToken ct)
    {
        var orderItem = await context.OrderItems.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (orderItem is null)
            throw new Exception("Order item not found");

        context.OrderItems.Remove(orderItem);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

