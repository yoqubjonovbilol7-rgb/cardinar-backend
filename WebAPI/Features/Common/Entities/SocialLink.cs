namespace WebAPI.Features.Common.Entities;

public class SocialLink : BaseEntity
{
  public string Title { get; set; } = null!;
  public string Icon { get; set; } = null!;
  public string Link { get; set; } = null!;
}