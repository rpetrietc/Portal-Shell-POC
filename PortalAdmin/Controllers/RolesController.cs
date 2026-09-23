using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalAdmin.ViewModels;
using PortalData.Data;
using PortalData.Models;

namespace PortalAdmin.Controllers;

public class RolesController : Controller
{
    private readonly PortalShellContext _portalShellContext;

    public RolesController(PortalShellContext portalShellContext)
    {
        _portalShellContext = portalShellContext;
    }

    public async Task<IActionResult> Index()
    {
        // Load all roles and their assigned portal links
        // for display in the administration application.
        var roles = await _portalShellContext.Roles
            .AsNoTracking()
            .Include(role => role.PortalLinks)
            .OrderBy(role => role.Name)
            .ToListAsync();

        return View(roles);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new RoleEditViewModel
        {
            // Load all portal links so the administrator can
            // choose which links should belong to the new role.
            AvailablePortalLinks = await _portalShellContext.PortalLinks
                .AsNoTracking()
                .OrderBy(link => link.DisplayOrder)
                .ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // The available links are not posted back with the form,
            // so reload them before displaying the form again.
            viewModel.AvailablePortalLinks =
                await _portalShellContext.PortalLinks
                    .AsNoTracking()
                    .OrderBy(link => link.DisplayOrder)
                    .ToListAsync();

            return View(viewModel);
        }

        // Load the selected portal links from the database.
        var selectedPortalLinks =
            await _portalShellContext.PortalLinks
                .Where(link =>
                    viewModel.SelectedPortalLinkIds.Contains(link.Id))
                .ToListAsync();

        var role = new Role
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            PortalLinks = selectedPortalLinks
        };

        // Save the role and its portal link relationships.
        _portalShellContext.Roles.Add(role);
        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}