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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PortalLink portalLink)
    {
        if (!ModelState.IsValid)
        {
            return View(portalLink);
        }

        // Add the new navigation record to the shared portal database.
        _portalShellContext.PortalLinks.Add(portalLink);
        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        // Load the existing record that the user wants to update.
        var portalLink = await _portalShellContext.PortalLinks.FindAsync(id);

        if (portalLink == null)
        {
            return NotFound();
        }

        return View(portalLink);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PortalLink portalLink)
    {
        if (id != portalLink.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(portalLink);
        }

        // Load the existing database record before applying the changes.
        var existingLink = await _portalShellContext.PortalLinks.FindAsync(id);

        if (existingLink == null)
        {
            return NotFound();
        }

        // Update only the fields that can be managed through this page.
        existingLink.Title = portalLink.Title;
        existingLink.Url = portalLink.Url;
        existingLink.DisplayOrder = portalLink.DisplayOrder;
        existingLink.IsEnabled = portalLink.IsEnabled;

        await _portalShellContext.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
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