namespace WebAPI.Features.Common.SocialLinks.Admin.UpdateSocialLink;

public class UpdateSocialLinkRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public IFormFile? Icon { get; set; }
}

