using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class FolderConfiguration : IEntityTypeConfiguration<Folder>
{
  public void Configure(EntityTypeBuilder<Folder> builder)
  {
    builder.HasKey(f => f.Id);

    builder.Property(f => f.Title)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(200);

    builder.Property(f => f.Description)
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(f => f.Icon)
      .HasColumnType("nvarchar")
      .HasMaxLength(100);

    builder.Property(f => f.Color)
      .HasColumnType("nvarchar")
      .HasMaxLength(7);

    builder.Property(f => f.Created_At)
      .IsRequired();

    builder.HasOne(f => f.User)
      .WithMany(u => u.Folders)
      .HasForeignKey(f => f.User_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(f => f.ParentFolder)
      .WithMany(f => f.SubFolders)
      .HasForeignKey(f => f.Parent_Folder_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(f => f.Tags)
      .WithMany(t => t.Folders)
      .UsingEntity(j => j.ToTable("FOLDER_TAG"));
  }
}
