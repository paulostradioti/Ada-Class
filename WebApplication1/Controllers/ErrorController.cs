using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
public class ErrorController : Controller
{
    [Route("Error/Status/{code}")]
    public IActionResult Status(int code)
    {
        if (code == 404) return View("~/Views/Shared/Errors/NotFound.cshtml");
        return View("Error");
    }
}
}
