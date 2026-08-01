using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Cart.CartItem.Admin.CreateCartItem;

public class CreateCartItemEndpoint(CardinarDbContext context)
    : Endpoint<CreateCartItemRequest, CreateCartItemResponse>
{
    public override void Configure()
    {
        Post("v1/admin/cart-items/create");
        Tags("Admin");
        AllowAnonymous();
        Options(x => x.WithTags("CartItems"));
    }

    public override async Task<CreateCartItemResponse> ExecuteAsync(CreateCartItemRequest req, CancellationToken ct)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);

        if (product is null)
            throw new Exception("Product not found.");

        var articul = await context.Articuls.FirstOrDefaultAsync(x => x.Id == req.ArticulId, ct);

        if (articul is null)
            throw new Exception("Articul not found.");

        var cartItem = new Entities.CartItem
        {
            ProductId = req.ProductId,
            ArticulId = req.ArticulId,
            Quantity = req.Quantity
        };

        context.CartItems.Add(cartItem);
        await context.SaveChangesAsync(ct);

        return new CreateCartItemResponse
        {
            Id = cartItem.Id,
            ProductId = cartItem.ProductId,
            ArticulId = cartItem.ArticulId,
            Quantity = cartItem.Quantity
        };
    }
}

