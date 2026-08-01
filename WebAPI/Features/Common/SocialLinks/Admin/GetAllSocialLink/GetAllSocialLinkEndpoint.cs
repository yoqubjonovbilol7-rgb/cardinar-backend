using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Common.SocialLinks.Admin.GetAllSocialLink;

public class GetAllSocialLinkEndpoint(CardinarDbContext context)
    : Endpoint<GetAllSocialLinkRequest, PaginatedResponse<GetAllSocialLinkResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/social-links/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("SocialLinks"));
    }

    public override async Task<PaginatedResponse<GetAllSocialLinkResponse>> ExecuteAsync(GetAllSocialLinkRequest req, CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.SocialLinks.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);

        var data = await query
            .OrderBy(x => x.Id)
            .Select(GetAllSocialLinkResponse.Project)
            .Skip(skip)
            .Take(take)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllSocialLinkResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}

