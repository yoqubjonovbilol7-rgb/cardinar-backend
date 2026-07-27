using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Features.Auth.Users.Public.Login;

public class LoginEndpoint(CardinarDbContext context) : Endpoint<LoginRequest, LoginResponse>
{
  public override void Configure()
  {
    Post("v1/public/auth/login");
    Options(opts => opts.WithTags("Auth"));
    Tags("Public");
    AllowAnonymous();
  }

  public override async Task<LoginResponse> ExecuteAsync(LoginRequest req, CancellationToken ct)
  {
    var user = await context.Users.SingleOrDefaultAsync(u => EF.Functions.ILike(u.Email, req.Login) || u.PhoneNumber == req.Login, ct);
    if (user == null)
      throw new Exception("Unauthorized.");

    if (user.Password != req.Password)
      throw new Exception("Unauthorized.");

    List<Claim> claims =
    [
      new("id", user.Id.ToString()),
      new("type", user.IsAdmin ? "admin" : "public")
    ];

    var handler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes("VeryVeryReliableSecretKeyWhichIsLongEnough");
    var descriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(3)),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
    };

    var response = new LoginResponse
    {
      AccessToken = handler.WriteToken(handler.CreateToken(descriptor)),
    };

    return response;
  }
}