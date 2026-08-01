namespace WebAPI.Features.Common.CarModel.Public.GetOneCarModel;

public class GetOneCarModelResponsePublic
{
    public int Id { get; set; }
    public int CarMakeId { get; set; }
    public string Title { get; set; } = null!;

    public static GetOneCarModelResponsePublic FromEntity(Entities.CarModel carModel) => new GetOneCarModelResponsePublic()
    {
        Id = carModel.Id,
        CarMakeId = carModel.CarMakeId,
        Title = carModel.Title
    };
}