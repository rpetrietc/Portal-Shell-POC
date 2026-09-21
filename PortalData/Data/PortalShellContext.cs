using Microsoft.EntityFrameworkCore;
using PortalData.Models;

namespace PortalData.Data;

public class PortalShellContext : DbContext
{
    public PortalShellContext(DbContextOptions<PortalShellContext> options)
        : base(options)
    {
    }

    public DbSet<PortalLink> PortalLinks => Set<PortalLink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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