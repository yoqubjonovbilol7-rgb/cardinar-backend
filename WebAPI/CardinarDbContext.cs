using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Enums;
using WebAPI.Features.Auth.Entities;
using WebAPI.Features.Branches.Entities;
using WebAPI.Features.Common.Entities;
using WebAPI.Features.Constructor.Entities;
using WebAPI.Features.Products.Entities;
using WebAPI.Features.Cart.Entities;
using WebAPI.Features.Requests.Entities;
using WebAPI.Features.Orders.Entities;
using OrderEntity = WebAPI.Features.Orders.Entities.Order;

namespace WebAPI;

public class CardinarDbContext(DbContextOptions<CardinarDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
  public DbSet<Branch> Branches { get; set; }
  public DbSet<SocialLink> SocialLinks { get; set; }
  public DbSet<PhoneNumber> PhoneNumbers { get; set; }
  public DbSet<StaticInfo> StaticInfos { get; set; }
  public DbSet<Banner> Banners { get; set; }

  public DbSet<ProductCategory> ProductCategories { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<ProductImage> ProductImages { get; set; }
  public DbSet<ProductColor> ProductColors { get; set; }
  public DbSet<Articul> Articuls { get; set; }

  public DbSet<CartItem> CartItems { get; set; }
  
  public DbSet<CustomModel> CustomModels { get; set; }
  public DbSet<CustomProduct> CustomProducts { get; set; }
  public DbSet<Materials> Materials { get; set; }
  public DbSet<Part> Parts { get; set; }


  public DbSet<OrderEntity> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }

  public DbSet<Color> Colors { get; set; }
  public DbSet<CarMake> CarMakes { get; set; }
  public DbSet<CarModel> CarModels { get; set; }
  
  public DbSet<Request> Requests { get; set; }
}