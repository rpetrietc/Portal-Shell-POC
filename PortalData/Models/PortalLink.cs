namespace PortalData.Models;

public class PortalLink
{
    public int Id { get; set; }

    // Display text shown to the user in the Portal.
    public string Title { get; set; } = string.Empty;

    // Destination URL for the link.
    public string Url { get; set; } = string.Empty;

    // Controls the order the links appear in the UI.
    public int DisplayOrder { get; set; }

    // Allows links to be enabled or disabled without deleting them.
    public bool IsEnabled { get; set; }

    // Roles that are allowed to see this portal link.
    public ICollection<Role> Roles { get; set; }
        = new List<Role>();
}