namespace WebAPI.Features.Common.CarModel.Admin.CreateCarModel;

public class CreateCarModelRequest
{
    public int CarMakeId { get; set; }
    public string Title { get; set; } = string.Empty;
}

