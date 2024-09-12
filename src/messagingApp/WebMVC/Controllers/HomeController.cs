using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;
using System.Security.Claims;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class HomeController(IToastNotification toastNotification): Controller
{
    public IActionResult Index()
    {
        toastNotification.AddSuccessToastMessage("Welcome to the chat app!");
		return View();
    }

    public IActionResult GetChatMessages()
    {
        return PartialView("_ChatMessages");
    }

    public IActionResult GetUserChatsAsync()
    {
        return PartialView("_UserChats");
    }

}
