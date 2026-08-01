namespace WebAPI.Features.Common.CarModel.Admin.GetOneCarModel;

public class GetOneCarModelResponse
{
    public int Id { get; set; }
    public int CarMakeId { get; set; }
    public string Title { get; set; } = null!;

    public static GetOneCarModelResponse FromEntity(Entities.CarModel carModel) => new GetOneCarModelResponse()
    {
        Id = carModel.Id,
        CarMakeId = carModel.CarMakeId,
        Title = carModel.Title
    };
}