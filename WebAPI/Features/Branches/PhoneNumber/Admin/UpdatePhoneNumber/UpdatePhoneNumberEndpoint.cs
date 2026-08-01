using Cardinar_Backend.Feature.Common.PhoneNumbers.Admin.UpdatePhoneNumber;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Branches.PhoneNumber.Admin.UpdatePhoneNumber;

public class UpdatePhoneNumberEndpoint(CardinarDbContext context)
    : Endpoint<UpdatePhoneNumberRequest, UpdatePhoneNumberResponse>
{
    public override void Configure()
    {
        Patch("/v1/admin/phone-numbers/update/{id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(opts => opts.WithTags("PhoneNumbers"));
    }

    public override async Task<UpdatePhoneNumberResponse> ExecuteAsync(UpdatePhoneNumberRequest req,
        CancellationToken ct)
    {
        var id = Route<int>("id");

        var phoneNumber = await context.PhoneNumbers.FirstOrDefaultAsync(p => p.Id == id, ct);

        if (phoneNumber == null)
        {
            throw new Exception($"Phone number with id {id} not found");
        }

        phoneNumber.Value = req.Value;
        await context.SaveChangesAsync(ct);
        return new UpdatePhoneNumberResponse
        {
            Id = phoneNumber.Id,
            Value = phoneNumber.Value
        };
    }
}