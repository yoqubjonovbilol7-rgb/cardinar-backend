using WebAPI.Core.Enums;

namespace WebAPI.Features.Constructor.Entities;

public class CustomModel : BaseEntity
{
    public Category Category { get; set; }

    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    // Navigation Properties
    public ICollection<CustomProduct> CustomProducts { get; set; } = new List<CustomProduct>();
}