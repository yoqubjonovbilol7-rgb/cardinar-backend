using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Cart.CartItem.Admin.DeleteCartItem;

public class DeleteCartItemEndpoint(CardinarDbContext context)
    : Endpoint<DeleteCartItemRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/cart-items/delete/{Id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CartItems"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeleteCartItemRequest req, CancellationToken ct)
    {
        var cartItem = await context.CartItems.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (cartItem is null)
            throw new Exception("Cart item not found");

        context.CartItems.Remove(cartItem);

        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;
    }
}

