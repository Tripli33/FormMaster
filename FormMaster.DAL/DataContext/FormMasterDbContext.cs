using FormMaster.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormMaster.DAL.DataContext;

public class FormMasterDbContext(DbContextOptions<FormMasterDbContext> options) : 
    IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, 
         IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Template> Templates { get; set; }
    public DbSet<Topic> Topics { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Template>()
            .HasOne(t => t.User)
            .WithMany(u => u.Templates);

        builder.Entity<Template>()
            .HasMany(t => t.AllowedUsers)
            .WithMany(u => u.AllowedTemplates);

        builder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
    }
}
