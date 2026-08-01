using WebAPI.Features.Constructor.Entities;
using WebAPI.Features.Products.Entities;

namespace WebAPI.Features.Common.Entities;

public class CarModel : BaseEntity
{
  
    public int CarMakeId { get; set; }

    public string Title { get; set; } = null!;

    // Navigation Properties
    public CarMake CarMake { get; set; } = null!;

    public ICollection<Articul> Articuls { get; set; } = new List<Articul>();

    public ICollection<CustomProduct> CustomProducts { get; set; } = new List<CustomProduct>();
}