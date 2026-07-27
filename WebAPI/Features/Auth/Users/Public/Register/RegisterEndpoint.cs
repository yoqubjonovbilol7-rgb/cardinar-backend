using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI.Features.Auth.Entities;

namespace WebAPI.Features.Auth.Users.Public.Register;

public class RegisterEndpoint(CardinarDbContext context) : Endpoint<RegisterRequest, Created<User>>
{
  public override void Configure()
  {
    Post("v1/public/auth/register");
    Options(opts => opts.WithTags("Auth"));
    Tags("Public");
  }

  public override async Task<Created<User>> ExecuteAsync(RegisterRequest req, CancellationToken ct)
  {
    var alreadyExists = await context.Users.AnyAsync(u => u.PhoneNumber == req.PhoneNumber || EF.Functions.ILike(u.Email, req.Email), ct);
    if (alreadyExists)
      throw new Exception("User with given Phone number or Email already exists");

    var newUser = req.ToEntity();
    context.Users.Add(newUser);
    await context.SaveChangesAsync(ct);
    return TypedResults.Created(string.Empty, newUser);
  }
}