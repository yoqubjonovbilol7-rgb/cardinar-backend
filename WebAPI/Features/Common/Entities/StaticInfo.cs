namespace WebAPI.Features.Common.Entities;

public class StaticInfo : BaseEntity
{
  
    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string WorkingHours { get; set; } = null!;

    public string Email { get; set; } = null!;
}