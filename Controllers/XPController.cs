using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music.Models;

namespace music.Controllers;

[SessionCheck]
public class XPController : Controller
{
    private MyContext _context;
    private readonly ILogger<XPController> _logger;

    public XPController(ILogger<XPController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public IActionResult DesktopPage()
    {
        return View("~/Views/Project/DesktopPage.cshtml");
    }
    [HttpGet]
    public IActionResult CalculatorPage()
    {
        return View("~/Views/Project/CalculatorPage.cshtml");
    }
    [HttpGet]
    public IActionResult MusicPlayer()
    {
        return View("~/Views/Project/MusicPlayer.cshtml");
    }
    [HttpGet]
    public IActionResult LoggingOff()
    {
        return View("~/Views/Project/LoggingOff.cshtml");
    }
    [HttpGet]
    public IActionResult Photo()
    {
        List<Photo> AllPhotos = _context.Photos.ToList();
        return View("~/Views/Project/Photo.cshtml", AllPhotos);
    }

    [HttpGet("XP/AddPhoto")]
    public IActionResult AddPhoto()
    {
        return View("~/Views/Project/AddPhoto.cshtml");
    }
    [HttpPost("XP/Create")]
    public IActionResult CreatePhoto(Photo newPhoto)
    {
        if (!ModelState.IsValid)
        {
            return View("NewPhoto");
        }
        newPhoto.UserId = (int)HttpContext.Session.GetInt32("UserId");
        _context.Add(newPhoto);
        _context.SaveChanges();
        return RedirectToAction("Photo");
    }
    [HttpPost("XP/{PhotoId}/delete")]
    public IActionResult DeletePhoto(int PhotoId)
    {
        Photo? ToBeDeleted = _context.Photos.FirstOrDefault(u => u.PhotoId == PhotoId);
        if (ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("Photo", "XP");
    }
}