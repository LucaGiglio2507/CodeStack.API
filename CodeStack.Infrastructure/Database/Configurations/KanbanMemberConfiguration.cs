using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class KanbanMemberConfiguration : IEntityTypeConfiguration<KanbanMember>
{
  public void Configure(EntityTypeBuilder<KanbanMember> builder)
  {
    builder.HasKey(km => km.Id);

    builder.Property(km => km.Role)
      .IsRequired();

    builder.Property(km => km.Joined_At)
      .IsRequired();

    builder.HasOne(km => km.User)
      .WithMany(u => u.KanbanMemberships)
      .HasForeignKey(km => km.User_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(km => km.Kanban)
      .WithMany(k => k.Members)
      .HasForeignKey(km => km.Kanban_Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
