using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
  public void Configure(EntityTypeBuilder<Group> builder)
  {
    builder.HasKey(g => g.Id);

    builder.HasMany(g => g.Participants)
      .WithMany(u => u.Groups)
      .UsingEntity(j => j.ToTable("Group_User"));
  }
}
