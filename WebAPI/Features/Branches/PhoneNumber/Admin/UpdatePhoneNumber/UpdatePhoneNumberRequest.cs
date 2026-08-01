using FastEndpoints;

namespace Cardinar_Backend.Feature.Common.PhoneNumbers.Admin.UpdatePhoneNumber;

public class UpdatePhoneNumberRequest
{
    [RouteParam]
    public string? Id { get; set; }
    public string? Value { get; set; }
}