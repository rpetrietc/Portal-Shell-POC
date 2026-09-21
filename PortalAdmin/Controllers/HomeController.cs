using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalData.Data;
using PortalData.Models;
using System.Diagnostics;
using PortalAdmin.Models;

namespace PortalAdmin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PortalShellContext _portalShellContext;

    public HomeController(
        ILogger<HomeController> logger,
        PortalShellContext portalShellContext)
    {
        _logger = logger;
        _portalShellContext = portalShellContext;
    }

    public async Task<IActionResult> Index()
    {
        // Load all portal links so the administration page can
        // show both enabled and disabled records.
        var portalLinks = await _portalShellContext.PortalLinks
            .AsNoTracking()
            .OrderBy(link => link.DisplayOrder)
            .ToListAsync();

        return View(portalLinks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId =
                Activity.Current?.Id ??
                HttpContext.TraceIdentifier
        });
    }
}