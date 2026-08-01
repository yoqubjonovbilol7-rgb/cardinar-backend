using WebAPI.Core.Enums;

namespace WebAPI.Features.Common.Entities;

public class Color : BaseEntity
{
    public string Title { get; set; } = null!;
    
    public string ColorCode { get; set; } = null!;
}