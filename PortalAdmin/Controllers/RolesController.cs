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
            // Reload the available links before redisplaying the form.
            viewModel.AvailablePortalLinks =
                await _portalShellContext.PortalLinks
                    .AsNoTracking()
                    .OrderBy(link => link.DisplayOrder)
                    .ToListAsync();

            return View(viewModel);
        }

        // Load the portal links selected for the new role.
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        // Load the role together with its current portal link assignments.
        var role = await _portalShellContext.Roles
            .Include(role => role.PortalLinks)
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role == null)
        {
            return NotFound();
        }

        var viewModel = new RoleEditViewModel
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,

            // Preselect the links currently assigned to this role.
            SelectedPortalLinkIds = role.PortalLinks
                .Select(link => link.Id)
                .ToList(),

            AvailablePortalLinks = await _portalShellContext.PortalLinks
                .AsNoTracking()
                .OrderBy(link => link.DisplayOrder)
                .ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        RoleEditViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            // Reload the available links before redisplaying the form.
            viewModel.AvailablePortalLinks =
                await _portalShellContext.PortalLinks
                    .AsNoTracking()
                    .OrderBy(link => link.DisplayOrder)
                    .ToListAsync();

            return View(viewModel);
        }

        // Load the existing role and its current relationships.
        var role = await _portalShellContext.Roles
            .Include(role => role.PortalLinks)
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role == null)
        {
            return NotFound();
        }

        role.Name = viewModel.Name;
        role.Description = viewModel.Description;

        // Remove the old link assignments before applying the new selection.
        role.PortalLinks.Clear();

        var selectedPortalLinks =
            await _portalShellContext.PortalLinks
                .Where(link =>
                    viewModel.SelectedPortalLinkIds.Contains(link.Id))
                .ToListAsync();

        foreach (var portalLink in selectedPortalLinks)
        {
            role.PortalLinks.Add(portalLink);
        }

        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}