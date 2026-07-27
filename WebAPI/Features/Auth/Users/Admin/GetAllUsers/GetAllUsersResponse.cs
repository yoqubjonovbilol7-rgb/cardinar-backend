using System.Linq.Expressions;
using WebAPI.Features.Auth.Entities;

namespace WebAPI.Features.Auth.Users.Admin.GetAllUsers;

public class GetAllUsersResponse
{
  public int Id { get; set; }
  public bool IsAdmin { get; set; }
  public string FullName { get; set; } = null!;
  public string PhoneNumber { get; set; } = null!;
  public string Email { get; set; } = null!;
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public static readonly Expression<Func<User, GetAllUsersResponse>> Project = u => new GetAllUsersResponse
  {
    Id = u.Id,
    FullName = u.FullName,
    PhoneNumber = u.PhoneNumber,
    Email = u.Email,
    IsAdmin = u.IsAdmin,
    CreatedAt = u.CreatedAt,
    UpdatedAt = u.UpdatedAt
  };
}