namespace WebAPI.Features.Common.CarMake.Admin.UpdateCarMake;

public class UpdateCarMakeRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}