namespace WebAPI.Features.Common.Color.Admin.UpdateColor;

public class UpdateColorRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ColorCode { get; set; } = string.Empty;
}

