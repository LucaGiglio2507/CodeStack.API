using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class KanbanColumnConfiguration : IEntityTypeConfiguration<KanbanColumn>
{
    public void Configure(EntityTypeBuilder<KanbanColumn> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("nvarchar")
            .HasMaxLength(100);

        builder.Property(c => c.Color)
            .HasColumnType("nvarchar")
            .HasMaxLength(50);

        builder.Property(c => c.Order)
            .IsRequired();

        builder.HasOne(c => c.Kanban)
            .WithMany(k => k.Columns)
            .HasForeignKey(c => c.Kanban_Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
