using Microsoft.EntityFrameworkCore;
using PortalData.Models;

namespace PortalData.Data;

public class PortalShellContext : DbContext
{
    public PortalShellContext(DbContextOptions<PortalShellContext> options)
        : base(options)
    {
    }

    // Navigation links managed by the portal.
    public DbSet<PortalLink> PortalLinks => Set<PortalLink>();

    // Roles used to group portal link access.
    public DbSet<Role> Roles => Set<Role>();

    // Users that can be assigned one or more roles.
    public DbSet<PortalUser> Users => Set<PortalUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Store portal user records in a table named Users.
        modelBuilder.Entity<PortalUser>()
            .ToTable("Users");

        // A role can contain many portal links, and the same portal link
        // can be assigned to more than one role.
        modelBuilder.Entity<Role>()
            .HasMany(role => role.PortalLinks)
            .WithMany(link => link.Roles)
            .UsingEntity("RolePortalLinks");

        // A user can have many roles, and the same role
        // can be assigned to more than one user.
        modelBuilder.Entity<PortalUser>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity("UserRoles");

        // Seed sample links used to demonstrate database-driven navigation.
        modelBuilder.Entity<PortalLink>().HasData(
            new PortalLink
            {
                Id = 1,
                Title = "Service Intake Demo",
                Url = "#",
                DisplayOrder = 1,
                IsEnabled = true
            },
            new PortalLink
            {
                Id = 2,
                Title = "Licensing Dashboard",
                Url = "#",
                DisplayOrder = 2,
                IsEnabled = true
            },
            new PortalLink
            {
                Id = 3,
                Title = "Application Status",
                Url = "#",
                DisplayOrder = 3,
                IsEnabled = true
            },
            new PortalLink
            {
                Id = 4,
                Title = "Document Requests",
                Url = "#",
                DisplayOrder = 4,
                IsEnabled = false
            }
        );
    }
}