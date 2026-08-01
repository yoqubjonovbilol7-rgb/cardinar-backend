namespace WebAPI.Features.Common.Entities;

public class Banner : BaseEntity
{
  
    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool IsActive { get; set; }
}