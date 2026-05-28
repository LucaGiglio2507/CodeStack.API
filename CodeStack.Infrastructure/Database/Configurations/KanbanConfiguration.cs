using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class KanbanConfiguration : IEntityTypeConfiguration<Kanban>
{
  public void Configure(EntityTypeBuilder<Kanban> builder)
  {
    builder.HasKey(k => k.Id);

    builder.Property(k => k.Name)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(100);

    builder.Property(k => k.Description)
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(k => k.Icon_url)
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(k => k.Created_At)
      .IsRequired();

    builder.HasOne(k => k.Creator)
      .WithMany(u => u.CreatedKanbans)
      .HasForeignKey(k => k.Creator_Id)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
