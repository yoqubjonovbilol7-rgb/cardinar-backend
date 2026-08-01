namespace WebAPI.Features.Common.SocialLinks.Admin.GetAllSocialLink;

public class GetAllSocialLinkRequest : PaginatedRequest
{
    public string? Search { get; set; }
}

