using Microsoft.AspNetCore.Mvc;

namespace PortalShell.Controllers;

public class DplDemoController : Controller
{
    public IActionResult Embedded()
    {
        return View();
    }
}