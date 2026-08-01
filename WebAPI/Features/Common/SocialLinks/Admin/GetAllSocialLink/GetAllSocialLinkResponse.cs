using System.Linq.Expressions;

namespace WebAPI.Features.Common.SocialLinks.Admin.GetAllSocialLink;

public class GetAllSocialLinkResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string? Icon { get; set; }

    public static Expression<Func<Entities.SocialLink, GetAllSocialLinkResponse>> Project =>
        x => new GetAllSocialLinkResponse
        {
            Id = x.Id,
            Title = x.Title,
            Link = x.Link,
            Icon = x.Icon
        };
}

