using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Branches.PhoneNumber.Admin.CreatePhoneNumber;

public class CreatePhoneNumberEndpoint(CardinarDbContext context) : Endpoint<CreatePhoneNumberRequest, CreatePhoneNumberResponse>
{
    public override void Configure()
    {
        Post("v1/admin/phone-numbers/create");
        Tags("Admin");
        AllowAnonymous();
        Options(opts => opts.WithTags("PhoneNumbers"));
    }

    public override async Task<CreatePhoneNumberResponse> ExecuteAsync(CreatePhoneNumberRequest req, CancellationToken ct)
    {
        var alreadyExists = await context.PhoneNumbers.AnyAsync(p => p.Value == req.Value, ct);

        if (alreadyExists)
        {
            throw new Exception("Phone number already exists");
        }

        var phoneNumber = new Entities.PhoneNumber
        {
            Value = req.Value
        };

        context.PhoneNumbers.Add(phoneNumber);
        await context.SaveChangesAsync(ct);

        return new CreatePhoneNumberResponse
        {
            Id = phoneNumber.Id,
            Value = phoneNumber.Value
        };
    }
}