using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.CoreMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortalShell.Data;
using PortalShell.Models;
using System.Diagnostics;

namespace PortalShell.Controllers;

public class HomeController : WebTemplateBaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly PortalShellContext _portalShellContext;

    public HomeController(
        ModelAccessor modelAccessor,
        ILogger<HomeController> logger,
        PortalShellContext portalShellContext)
        : base(modelAccessor)
    {
        _logger = logger;
        _portalShellContext = portalShellContext;

        // Sets the application name displayed by the
        // Government of Canada Web Template.
        WebTemplateModel.HeaderTitle = "CivAv Portal Proof Of Concept";
    }

    public async Task<IActionResult> Index()
    {
        // Load only enabled links from the database.
        // DisplayOrder controls the order they will appear on the page.
        var portalLinks = await _portalShellContext.PortalLinks
            .Where(link => link.IsEnabled)
            .OrderBy(link => link.DisplayOrder)
            .ToListAsync();

        // Pass the database-generated links to the Home view.
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