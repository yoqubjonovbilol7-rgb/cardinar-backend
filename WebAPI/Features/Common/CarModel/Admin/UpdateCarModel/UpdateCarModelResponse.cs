namespace WebAPI.Features.Common.CarModel.Admin.UpdateCarModel;

public class UpdateCarModelResponse
{
    public int Id { get; set; }
    public int CarMakeId { get; set; }
    public string Title { get; set; } = string.Empty;
}

