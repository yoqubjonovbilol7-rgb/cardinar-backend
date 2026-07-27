using System.ComponentModel.DataAnnotations;

namespace WebAPI.Features.Auth.Entities;

public class User : BaseEntity
{
  [StringLength(64)]
  public string FullName { get; set; } = null!;

  [StringLength(16)]
  public string PhoneNumber { get; set; } = null!;

  [StringLength(64)]
  public string Email { get; set; } = null!;

  [StringLength(128)]
  public string Password { get; set; } = null!;

  public bool IsAdmin { get; set; }
}