namespace WebAPI.Features.Common.SocialLinks.Admin.UpdateSocialLink;

public class UpdateSocialLinkResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string? Icon { get; set; }
}

