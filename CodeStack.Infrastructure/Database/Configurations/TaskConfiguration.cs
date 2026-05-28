using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = CodeStack.Domain.Entities.Task;

namespace CodeStack.Infrastructure.Database.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
  public void Configure(EntityTypeBuilder<Task> builder)
  {
    builder.HasKey(t => t.Id);

    builder.Property(t => t.Title)
      .IsRequired()
      .HasColumnType("nvarchar")
      .HasMaxLength(200);

    builder.Property(t => t.Description)
      .HasColumnType("nvarchar")
      .HasMaxLength(1000);

    builder.Property(t => t.Created_at)
      .IsRequired();

    builder.HasOne(t => t.Kanban)
      .WithMany(k => k.Tasks)
      .HasForeignKey(t => t.Kanban_Id)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(t => t.AssignedTo)
      .WithMany()
      .HasForeignKey(t => t.User_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(t => t.Tags)
      .WithMany(tg => tg.Tasks)
      .UsingEntity(j => j.ToTable("TASK_TAG"));
  }
}
