using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class HomeController: Controller
{
    public async Task<IActionResult> Index()
    {
        
        return View();
    }

    public async Task<IActionResult> GetChatMessages(Guid chatId)
    {

        return PartialView("_ChatMessages");
    }

    public async Task<IActionResult> GetUserChatsAsync()
    {

        return PartialView("_UserChats");
    }

}
