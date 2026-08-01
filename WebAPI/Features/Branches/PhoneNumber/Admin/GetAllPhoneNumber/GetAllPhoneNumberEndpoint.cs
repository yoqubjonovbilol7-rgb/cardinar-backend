using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Branches.PhoneNumber.Admin.GetAllPhoneNumber;

public class GetAllPhoneNumberEndpoint(CardinarDbContext context)
    : Endpoint<GetAllPhoneNumberRequest, PaginatedResponse<GetAllPhoneNumberResponse>>
{
    public override void Configure()
    {
        Get("v1/admin/phone-numbers/get-all");
        Policies("Admin");
        Tags("Admin");
        Options(opts => opts.WithTags("PhoneNumbers"));
    }

    public override async Task<PaginatedResponse<GetAllPhoneNumberResponse>> ExecuteAsync(
        GetAllPhoneNumberRequest req,
        CancellationToken ct)
    {
        var currentPage = req.Page ?? 1;
        var pageSize = req.Size ?? 10;
        var skip = (currentPage - 1) * pageSize;

        IQueryable<Entities.PhoneNumber> query = context.PhoneNumbers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(req.Search))
        {
            query = query.Where(x =>
                EF.Functions.ILike(x.Value, $"%{req.Search}%"));
        }

        var totalCount = await query.CountAsync(ct);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var data = await query
            .OrderBy(x => x.Id)
            .Skip(skip)
            .Take(pageSize)
            .Select(GetAllPhoneNumberResponse.Project)
            .ToArrayAsync(ct);

        return PaginatedResponse<GetAllPhoneNumberResponse>.BuildFrom(
            totalCount,
            totalPages,
            currentPage,
            data);
    }
}