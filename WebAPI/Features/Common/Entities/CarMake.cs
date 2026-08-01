using WebAPI.Features.Constructor.Entities;

namespace WebAPI.Features.Common.Entities;

public class CarMake : BaseEntity
{
  
    public string Title { get; set; } = null!;

    public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();

    public ICollection<CustomProduct> CustomProducts { get; set; } = new List<CustomProduct>();
}