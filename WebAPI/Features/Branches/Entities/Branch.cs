using WebAPI.Core.Enums;

namespace WebAPI.Features.Branches.Entities;

public class Branch
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string? District { get; set; }

    public string Region { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public bool IsActive { get; set; } = true;

    public BranchType BranchType { get; set; }
}