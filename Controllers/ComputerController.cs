using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music.Models;

namespace music.Controllers;

public class ComputerController : Controller
{
    private MyContext _context;
    private readonly ILogger<ComputerController> _logger;

    public ComputerController(ILogger<ComputerController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public ViewResult Ups()
    {
        return View("~/Views/Project/Ups.cshtml");
    }
    [HttpGet]
    public IActionResult LoadingPage()
    {
        return View("~/Views/Project/LoadingPage.cshtml");
    }
    [HttpGet]
    public IActionResult ShuttingDown()
    {
        return View("~/Views/Project/ShuttingDown.cshtml");
    }
}