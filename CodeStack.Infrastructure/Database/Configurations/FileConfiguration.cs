using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
  public void Configure(EntityTypeBuilder<File> builder)
  {
    builder.HasKey(f => f.Id);

    builder.Property(f => f.Name)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(255);

    builder.Property(f => f.Url)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(f => f.Mime_type)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(100);

    builder.Property(f => f.Uploaded_At)
      .IsRequired();

    builder.HasOne(f => f.User)
      .WithMany(u => u.Files)
      .HasForeignKey(f => f.User_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(f => f.Folder)
      .WithMany(fo => fo.Files)
      .HasForeignKey(f => f.Folder_Id)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
