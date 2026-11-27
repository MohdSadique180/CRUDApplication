using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace CrudApplication.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Products");
        }

        return View();
    }

    public IActionResult GoogleLogin()
    {
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Action("Index", "Products") ?? "/"
        };

        return Challenge(props, GoogleDefaults.AuthenticationScheme);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
