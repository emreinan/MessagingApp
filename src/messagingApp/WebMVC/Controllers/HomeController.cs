using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;
using System.Security.Claims;
using WebMVC.Models;
using WebMVC.Services.Chat;
using WebMVC.Services.Message;

namespace WebMVC.Controllers;

public class HomeController(/*IToastNotification toastNotification ,*/
    IChatService chatService,
    IMessageService messageService
    ) : Controller
{
	public async Task<IActionResult> Index()
    {
        HomeViewModel model = new();
        model.UserChats = await chatService.GetUserChats(GetUserId());

        model.UserId = GetUserId();
        model.Nickname = GetUserNickname();

        return View(model);
    }

    public async Task<IActionResult> GetChatMessages(Guid chatId)
    {
        var userId = GetUserId();
		var chatMessages = await messageService.GetChatMessagesAsync(chatId);
        chatMessages.ForEach(m => m.IsCurrentUser = m.UserId == userId);
		return PartialView("_ChatMessages", chatMessages);
    }

    public async Task<IActionResult> GetUserChats()
    {
        var userId = GetUserId();
		var chats = await chatService.GetUserChats(userId);
		return PartialView("_UserChats", chats);
    }

    public async Task<IActionResult> CreateGroup(string groupName)
    {
        await chatService.CreateGroupAsync(groupName);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> JoinGroup(string code)
    {
        var userId = GetUserId();
        await chatService.JoinGroupAsync(Guid.Parse(code), userId);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> GetChatHeader(Guid chatId)
    {
        var chatDetails = await chatService.GetChatDetails(chatId);
        return PartialView("_ChatHeader", chatDetails);
    }

    public async Task<IActionResult> GetChatDetails(Guid chatId)
    {
        var chatDetails = await chatService.GetChatDetails(chatId);
        return PartialView("_ChatDetailModal", chatDetails);
    }

    public Guid GetUserId()
    {
        if (!User.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("Kullanýcý kimliði doðrulanmamýþ.");
        }

        var userClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userClaim == null || string.IsNullOrEmpty(userClaim.Value))
        {
            throw new ArgumentNullException(nameof(userClaim), "Kullanýcý ID'si bulunamadý.");
        }

        return Guid.Parse(userClaim.Value);
    }

    private string GetUserNickname()
    {
		return User.FindFirst(ClaimTypes.Name).Value;
	}

}
