using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Core;

public abstract class BaseEntity
{
  [Key]
  public int Id { get; set; }

  [Column(TypeName = "timestamptz")]
  public DateTime CreatedAt { get; set; }

  [Column(TypeName = "timestamptz")]
  public DateTime UpdatedAt { get; set; }
}