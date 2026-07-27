using FastEndpoints;
using FluentValidation;

namespace WebAPI.Features.Auth.Users.Public.Login;

public class LoginRequest
{
  public string Login { get; set; } = null!;
  public string Password { get; set; } = null!;
}

public class LoginRequestValidator : Validator<LoginRequest>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Password).MinimumLength(3);
  }
}