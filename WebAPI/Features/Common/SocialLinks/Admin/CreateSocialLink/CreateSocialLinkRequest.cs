using WebAPI.Features.Common.Entities;

namespace WebAPI.Features.Common.SocialLinks.Admin.CreateSocialLink;

public class CreateSocialLinkRequest
{
  public string Title { get; set; } = null!;
  public string Link { get; set; } = null!;
  public IFormFile Icon { get; set; } = null!;

  public SocialLink ToEntity(string icon) => new()
  {
    Title = Title,
    Link = Link,
    Icon = icon
  };
}