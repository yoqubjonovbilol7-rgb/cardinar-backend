using WebAPI.Features.Auth.Entities;

namespace WebAPI.Features.Requests.Entities;

public class Request : BaseEntity
{
  
    public int? UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string? Comments { get; set; }

    public User? User { get; set; }
}