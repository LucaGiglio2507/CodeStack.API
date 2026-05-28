using CodeStack.Domain.Entities;
using CodeStack.Domain.Enums;
using CodeStack.Security.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  private readonly PasswordHasherService _HashedPassword = new PasswordHasherService();
  public static readonly Guid AdminId = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d");

  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder.Property(u => u.Name)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(50);

    builder.Property(u => u.First_name)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(50);

    builder.Property(u => u.Email)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(250);

    builder.HasIndex(u => u.Email).IsUnique();

    builder.Property(u => u.Password)
      .IsRequired()
      .HasMaxLength(100);

    builder.Property(u => u.Role)
      .IsRequired();

    builder.Property(u => u.Avatar_Url)
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(u => u.Created_At)
      .IsRequired();

  }
}
