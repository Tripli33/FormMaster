using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.DAL.DataContext;

public class FormMasterDbContext(DbContextOptions<FormMasterDbContext> options) : 
    IdentityDbContext<User, UserRole, int>(options)
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Topic> Topics { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Template>()
            .HasOne(t => t.User)
            .WithMany(u => u.Templates);

        builder.Entity<Template>()
            .HasMany(t => t.AllowedUsers)
            .WithMany(u => u.AllowedTemplates);

        base.OnModelCreating(builder);
    }
}
