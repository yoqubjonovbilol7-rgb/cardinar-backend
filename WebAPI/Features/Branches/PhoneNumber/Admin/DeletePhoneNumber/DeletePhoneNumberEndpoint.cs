using Cardinar_Backend.Feature.Common.PhoneNumbers.Admin.DeletePhoneNumber;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Features.Branches.PhoneNumber.Admin.DeletePhoneNumber;

public class DeletePhoneNumberEndpoint(CardinarDbContext context) : Endpoint<DeletePhoneNumberRequest,EmptyResponse>
{
    public override void Configure()
    {
        Delete("v1/admin/phone-number/delete{id:int}");
        Tags("Admin");
        AllowAnonymous();
        Options(options => options.WithTags("PhoneNumbers"));
    }

    public override async Task<EmptyResponse> ExecuteAsync(DeletePhoneNumberRequest req, CancellationToken ct)
    {
        var phoneNumber = await context.PhoneNumbers.SingleOrDefaultAsync(p => p.Id == req.Id, ct);
        if (phoneNumber == null)
        {
            throw new Exception("Phone number does not exist.");
        }

        context.PhoneNumbers.Remove(phoneNumber);
        await context.SaveChangesAsync(ct);

        return EmptyResponse.Instance;

    }
}