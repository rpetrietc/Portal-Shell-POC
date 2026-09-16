using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.CoreMVC.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortalShell.Models;

namespace PortalShell.Controllers;

public class HomeController : WebTemplateBaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ModelAccessor modelAccessor,
        ILogger<HomeController> logger)
        : base(modelAccessor)
    {
        _logger = logger;

        // Sets the application name displayed by the
        // Government of Canada Web Template.
        WebTemplateModel.HeaderTitle = "Portal Shell";
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
