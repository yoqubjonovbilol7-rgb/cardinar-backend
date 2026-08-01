namespace WebAPI.Features.Common.CarModel.Admin.CreateCarModel;

public class CreateCarModelResponse
{
    public int Id { get; set; }
    public int CarMakeId { get; set; }
    public string Title { get; set; } = string.Empty;
}

