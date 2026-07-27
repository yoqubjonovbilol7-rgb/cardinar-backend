using Microsoft.EntityFrameworkCore;
using WebAPI.Features.Auth.Entities;
using WebAPI.Features.Common.Entities;

namespace WebAPI;

public class CardinarDbContext(DbContextOptions<CardinarDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<SocialLink> SocialLinks { get; set; }
}