using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalData.Data;

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
}