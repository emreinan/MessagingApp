using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class AuthController : Controller
{
    [HttpGet("/Login")]
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Verify()
    {
        return View();
    }


}
