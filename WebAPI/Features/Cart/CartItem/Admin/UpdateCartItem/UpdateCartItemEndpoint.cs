using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Cart.CartItem.Admin.UpdateCartItem;

public class UpdateCartItemEndpoint(CardinarDbContext context)
    : Endpoint<UpdateCartItemRequest, UpdateCartItemResponse>
{
    public override void Configure()
    {
        Patch("v1/admin/cart-items/update");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CartItems"));
    }

    public override async Task<UpdateCartItemResponse> ExecuteAsync(UpdateCartItemRequest req, CancellationToken ct)
    {
        var cartItem = await context.CartItems.FirstOrDefaultAsync(x => x.Id == req.Id, ct);

        if (cartItem is null)
            throw new Exception("Cart item not found.");

        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.ArticulId, ct);

        if (articul is null)
            throw new Exception("Articul not found.");

        cartItem.ProductId = req.ProductId;
        cartItem.ArticulId = req.ArticulId;
        cartItem.Quantity = req.Quantity;

        await context.SaveChangesAsync(ct);

        return new UpdateCartItemResponse
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ArticulId = cartItem.ArticulId,
            Quantity = cartItem.Quantity
        };
    }
}

