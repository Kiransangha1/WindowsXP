using System.Diagnostics;
// using AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using music.Models;

namespace music.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;

    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("users")]
    public IActionResult Register()
    {
        return View("Register");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost("users/register")]
    public IActionResult RegisterUser(User newUser)
    {
        if (!ModelState.IsValid)
        {
            return View("Register", newUser);
        }
        PasswordHasher<User> hasher = new();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);
        _context.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UserId", newUser.UserId);
        HttpContext.Session.SetString("Username", $"{newUser.Name}");
        return RedirectToAction("DesktopPage", "XP");

    }

    [HttpGet]
    public IActionResult LogAndReg()
    {
        List<User> AllUsers = _context.Users.ToList();
        return View("~/Views/Project/LogAndReg.cshtml", AllUsers);
    }

    [HttpPost("User/Login")]
    public IActionResult LoginUser(LogUser logAttempt)
    {
        User? OneUser = _context.Users.FirstOrDefault(u => u.UserId == logAttempt.UserId);
        if (!ModelState.IsValid)
        {
            return View("Login", OneUser);
            // return View("Login", "User");
        }
        if (OneUser == null)
        {
            ModelState.AddModelError("LogPassword", "Invalid credentials");
            return View("Login", OneUser);
        }
        PasswordHasher<LogUser> hasher = new();
        PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(logAttempt, OneUser.Password, logAttempt.LogPassword);
        if (pwCompareResult == 0)
        {
            ModelState.AddModelError("LogPassword", "Password Incorrect");
            return View("Login", OneUser);
        }
        HttpContext.Session.SetInt32("UserId", OneUser.UserId);
        HttpContext.Session.SetString("Username", $"{OneUser.Name}");
        return RedirectToAction("DesktopPage", "XP");
    }

    [HttpGet("User/Login/{UserId}")]
    public IActionResult Login(int UserId)
    {
        // User? OneUser = _context.Users.FirstOrDefault(u => u.UserId == logAttempt.UserId);
        User? OneUser = _context.Users.FirstOrDefault(u => u.UserId == UserId);
        if (OneUser == null)
        {
            return NotFound();
        }
        return View("Login", OneUser);
    }

    [HttpPost("users/{UserId}/delete")]
    public IActionResult DeleteUser(int UserId)
    {
        User? ToBeDeleted = _context.Users.FirstOrDefault(u => u.UserId == UserId);
        if (ToBeDeleted != null)
        {
            _context.Remove(ToBeDeleted);
            _context.SaveChanges();
        }
        return RedirectToAction("LogAndReg", "User");
    }

    [SessionCheck]
    [HttpPost("")]
    public RedirectToActionResult Logout()
    {
        // HttpContext.Session.Clear();
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("LogAndReg");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}