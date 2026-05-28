using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
  public void Configure(EntityTypeBuilder<Tag> builder)
  {
    builder.HasKey(t => t.Id);

    builder.Property(t => t.Name)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(50);

    builder.Property(t => t.Color)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(7);
  }
}
