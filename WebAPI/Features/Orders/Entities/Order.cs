using WebAPI.Core.Enums;
using WebAPI.Features.Auth.Entities;
using WebAPI.Features.Branches.Entities;

namespace WebAPI.Features.Orders.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }

    public int BranchId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public bool Delivery { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public OrderStatus Status { get; set; }
    
    public User User { get; set; } = null!;

    public Branch Branch { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}