using CodeStack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeStack.Infrastructure.Database.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
  public void Configure(EntityTypeBuilder<Message> builder)
  {
    builder.HasKey(m => m.Id);

    builder.Property(m => m.Content)
      .IsRequired()
      .HasColumnType("nvarchar(max)");

    builder.Property(m => m.CreatedAt)
      .IsRequired();

    builder.HasOne(m => m.Sender)
      .WithMany(u => u.SentMessages)
      .HasForeignKey(m => m.Sender_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(m => m.Receiver)
      .WithMany(u => u.ReceivedMessages)
      .HasForeignKey(m => m.Receiver_Id)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(m => m.Group)
      .WithMany(g => g.Messages)
      .HasForeignKey(m => m.Group_Id)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
