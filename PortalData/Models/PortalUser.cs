namespace PortalData.Models;

public class PortalUser
{
    public int Id { get; set; }

    // Friendly name displayed for the user in the administration application.
    public string DisplayName { get; set; } = string.Empty;

    // Email address used to identify the user for this proof of concept.
    public string Email { get; set; } = string.Empty;

    // Roles assigned to this user.
    public ICollection<Role> Roles { get; set; }
        = new List<Role>();
}