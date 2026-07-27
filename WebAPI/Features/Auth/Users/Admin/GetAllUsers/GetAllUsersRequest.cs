namespace WebAPI.Features.Auth.Users.Admin.GetAllUsers;

// REPR - Request-EndPoint-Response
public class GetAllUsersRequest : PaginatedRequest
{
  public string? Search { get; set; }
  public bool? IsAdmin { get; set; }
}