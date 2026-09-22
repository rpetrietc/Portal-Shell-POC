namespace PortalData.Models;

public class Role
{
    public int Id { get; set; }

    // Name used to identify the role in the administration application.
    public string Name { get; set; } = string.Empty;

    // Optional description explaining the purpose of the role.
    public string Description { get; set; } = string.Empty;

    // Portal links that are assigned to this role.
    public ICollection<PortalLink> PortalLinks { get; set; }
        = new List<PortalLink>();

    // Users that are assigned to this role.
    public ICollection<PortalUser> Users { get; set; }
        = new List<PortalUser>();
}