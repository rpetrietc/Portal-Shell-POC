using System.ComponentModel.DataAnnotations;
using PortalData.Models;

namespace PortalAdmin.ViewModels;

public class RoleEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    // Identifiers for the portal links selected for this role.
    public List<int> SelectedPortalLinkIds { get; set; } = new();

    // Portal links available for assignment in the administration form.
    public List<PortalLink> AvailablePortalLinks { get; set; } = new();
}