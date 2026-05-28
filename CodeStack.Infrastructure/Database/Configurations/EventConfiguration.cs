using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.Title)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(200);

    builder.Property(e => e.Description)
      .HasColumnType("nvarchar")
      .HasMaxLength(1000);

    builder.Property(e => e.Starts_At)
      .IsRequired();

    builder.HasOne(e => e.User)
      .WithMany(u => u.Events)
      .HasForeignKey(e => e.User_Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
