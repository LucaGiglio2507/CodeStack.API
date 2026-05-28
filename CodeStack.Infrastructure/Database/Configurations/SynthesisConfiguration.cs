using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class SynthesisConfiguration : IEntityTypeConfiguration<Synthesis>
{
  public void Configure(EntityTypeBuilder<Synthesis> builder)
  {
    builder.HasKey(s => s.Id);

    builder.Property(s => s.Title)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(200);

    builder.Property(s => s.Description)
      .HasColumnType("nvarchar")
      .HasMaxLength(500);

    builder.Property(s => s.Content)
      .HasColumnType("nvarchar(max)");

    builder.Property(s => s.Created_At)
      .IsRequired();

    builder.HasOne(s => s.User)
      .WithMany(u => u.Syntheses)
      .HasForeignKey(s => s.User_Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
