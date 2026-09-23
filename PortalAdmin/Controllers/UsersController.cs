using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalAdmin.ViewModels;
using PortalData.Data;
using PortalData.Models;

namespace PortalAdmin.Controllers;

public class UsersController : Controller
{
    private readonly PortalShellContext _portalShellContext;

    public UsersController(PortalShellContext portalShellContext)
    {
        _portalShellContext = portalShellContext;
    }

    public async Task<IActionResult> Index()
    {
        // Load all users and their assigned roles
        // for display in the administration application.
        var users = await _portalShellContext.Users
            .AsNoTracking()
            .Include(user => user.Roles)
            .OrderBy(user => user.DisplayName)
            .ToListAsync();

        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new UserEditViewModel
        {
            // Load all roles so the administrator can
            // choose which roles belong to the new user.
            AvailableRoles = await _portalShellContext.Roles
                .AsNoTracking()
                .OrderBy(role => role.Name)
                .ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        UserEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            // Reload roles before redisplaying the form.
            viewModel.AvailableRoles =
                await _portalShellContext.Roles
                    .AsNoTracking()
                    .OrderBy(role => role.Name)
                    .ToListAsync();

            return View(viewModel);
        }

        // Load the roles selected for the new user.
        var selectedRoles =
            await _portalShellContext.Roles
                .Where(role =>
                    viewModel.SelectedRoleIds.Contains(role.Id))
                .ToListAsync();

        var user = new PortalUser
        {
            DisplayName = viewModel.DisplayName,
            Email = viewModel.Email,
            Roles = selectedRoles
        };

        // Save the user and the selected role relationships.
        _portalShellContext.Users.Add(user);
        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        // Load the user together with their current role assignments.
        var user = await _portalShellContext.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new UserEditViewModel
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,

            // Preselect the roles currently assigned to this user.
            SelectedRoleIds = user.Roles
                .Select(role => role.Id)
                .ToList(),

            AvailableRoles = await _portalShellContext.Roles
                .AsNoTracking()
                .OrderBy(role => role.Name)
                .ToListAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UserEditViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            // Reload roles before redisplaying the form.
            viewModel.AvailableRoles =
                await _portalShellContext.Roles
                    .AsNoTracking()
                    .OrderBy(role => role.Name)
                    .ToListAsync();

            return View(viewModel);
        }

        // Load the existing user and their current relationships.
        var user = await _portalShellContext.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        user.DisplayName = viewModel.DisplayName;
        user.Email = viewModel.Email;

        // Remove the existing assignments before applying
        // the administrator's new role selection.
        user.Roles.Clear();

        var selectedRoles =
            await _portalShellContext.Roles
                .Where(role =>
                    viewModel.SelectedRoleIds.Contains(role.Id))
                .ToListAsync();

        foreach (var role in selectedRoles)
        {
            user.Roles.Add(role);
        }

        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}