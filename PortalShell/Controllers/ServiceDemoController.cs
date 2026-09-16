using Microsoft.AspNetCore.Mvc;

namespace PortalShell.Controllers;

public class ServiceDemoController : Controller
{
    public IActionResult Embedded()
    {
        return View();
    }
}