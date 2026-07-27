using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Auth.Users.Admin.GetAllUsers;

public class GetAllUsersEndpoint(CardinarDbContext context) : Endpoint<GetAllUsersRequest, PaginatedResponse<GetAllUsersResponse>>
{
  public override void Configure()
  {
    Get("v1/admin/users/get-all");
    Policies("Admin");
    Tags("Admin");
    Options(opts => opts.WithTags("Users"));
  }

  public override async Task<PaginatedResponse<GetAllUsersResponse>> ExecuteAsync(GetAllUsersRequest req, CancellationToken ct)
  {
    var currentPage = req.Page ?? 1;
    var take = req.Size ?? 10;
    var skip = (currentPage - 1) * take;

    var query = context.Users.AsNoTracking();

    if (!string.IsNullOrEmpty(req.Search))
      query = query.Where(u => EF.Functions.ILike(u.FullName, $"%{req.Search}%") ||
                               EF.Functions.ILike(u.Email, $"%{req.Search}%") ||
                               EF.Functions.Like(u.PhoneNumber, $"%{req.Search}%"));

    if (req.IsAdmin != null)
      query = query.Where(u => u.IsAdmin == req.IsAdmin);

    var totalCount = await query.CountAsync(ct);
    var totalPages = (int)Math.Ceiling((double)totalCount / (req.Size ?? 10));
    var data = await query.Select(GetAllUsersResponse.Project).Skip(skip).Take(take).ToArrayAsync(ct);

    return PaginatedResponse<GetAllUsersResponse>.BuildFrom(totalCount, totalPages, currentPage, data);
  }
}