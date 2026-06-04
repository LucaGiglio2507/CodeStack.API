using Microsoft.EntityFrameworkCore;
using DomainTask = CodeStack.Domain.Entities.Task;
using DomainFile = CodeStack.Domain.Entities.File;
using CodeStack.Domain.Entities;

namespace CodeStack.Infrastructure.Database.Context;

public class CodeStackDBContext : DbContext
{
  public CodeStackDBContext(DbContextOptions<CodeStackDBContext> options) : base(options) { }

  public DbSet<User> Users { get; set; } = null!;
  public DbSet<Kanban> Kanbans { get; set; } = null!;
  public DbSet<KanbanMember> KanbanMembers { get; set; } = null!;
  public DbSet<DomainTask> Tasks { get; set; } = null!;
  public DbSet<Tag> Tags { get; set; } = null!;
  public DbSet<Message> Messages { get; set; } = null!;
  public DbSet<Group> Groups { get; set; } = null!;
  public DbSet<Event> Events { get; set; } = null!;
  public DbSet<Synthesis> Syntheses { get; set; } = null!;
  public DbSet<DomainFile> Files { get; set; } = null!;
  public DbSet<Folder> Folders { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeStackDBContext).Assembly);
  }
}
