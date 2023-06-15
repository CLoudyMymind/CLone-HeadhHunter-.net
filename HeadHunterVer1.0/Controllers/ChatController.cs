using HeadHunterVer1._0.Context;
using HeadHunterVer1._0.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeadHunterVer1._0.Controllers;

public class ChatController : Controller
{
    private readonly HeadHunterContext _db;

    public ChatController(HeadHunterContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult CreateMessage()
    {
        var data = _db.Chats.FirstOrDefault();
        var dataChat = new MessageViewModel()
        {
            MessageText = data.Message
        };
        return View(dataChat);
    }
}