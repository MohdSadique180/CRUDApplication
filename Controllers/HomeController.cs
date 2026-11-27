using Microsoft.AspNetCore.Mvc;

namespace CrudApplication.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
