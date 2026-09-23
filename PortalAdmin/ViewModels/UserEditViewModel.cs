using System.ComponentModel.DataAnnotations;
using PortalData.Models;

namespace PortalAdmin.ViewModels;

public class UserEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string DisplayName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Identifiers for the roles selected for this user.
    public List<int> SelectedRoleIds { get; set; } = new();

    // Roles available for assignment in the administration form.
    public List<Role> AvailableRoles { get; set; } = new();
}