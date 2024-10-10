using FormMaster.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.DAL.DataContext;

public class FormMasterDbContext(DbContextOptions<FormMasterDbContext> options) : DbContext(options)
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Template>()
            .HasOne(t => t.User)
            .WithMany(u => u.Templates);

        modelBuilder.Entity<Template>()
            .HasMany(t => t.AllowedUsers)
            .WithMany(u => u.AllowedTemplates);

        base.OnModelCreating(modelBuilder);
    }
}
