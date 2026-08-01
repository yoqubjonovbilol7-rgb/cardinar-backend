using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Branches.PhoneNumber.Public.GetAllPhoneNumbers;

public class GetAllPhoneNumberEndpointPublic(CardinarDbContext context)
    : Endpoint<GetAllPhoneNumberRequestPublic, PaginatedResponse<GetAllPhoneNumberResponsePublic>>
{
    public override void Configure()
    {
        Get("v1/public/phone-number/get-all-phone-number");
        Policies("Public");
        Tags("Public");
        Options(opts => opts.WithTags("PhoneNumbers"));
    }

    public override async Task<PaginatedResponse<GetAllPhoneNumberResponsePublic>> ExecuteAsync(
        GetAllPhoneNumberRequestPublic req,
        CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var take = req.Size ?? 10;
        var skip = (currentPage - 1) * take;

        var query = context.PhoneNumbers.AsNoTracking();

        if (!string.IsNullOrEmpty(req.Search))
            query = query.Where(u => EF.Functions.ILike(u.Value, $"%{req.Search}%"));

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / take);
        var data = await query.Select(GetAllPhoneNumberResponsePublic.Project).Skip(skip).Take(take).ToArrayAsync(ct);

        return PaginatedResponse<GetAllPhoneNumberResponsePublic>.BuildFrom(totalCount, totalPages, currentPage, data);
    }
}