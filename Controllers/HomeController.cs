using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotNET_lab_3.Models;
using Lab3.Models;

namespace dotNET_lab_3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PhoneBookService _phoneBook;

    public HomeController(ILogger<HomeController> logger, PhoneBookService phoneBook)
    {
        _logger = logger;
        _phoneBook = phoneBook;
    }

    public IActionResult Index()
    {
        return View(_phoneBook.GetContacts());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _phoneBook.Add(contact);
            return RedirectToAction("Index");
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var contact = _phoneBook.GetContact(id);
        if (contact == null)
        {
            return NotFound();
        }
        _phoneBook.Remove(id);
        return RedirectToAction("Index");
    }
}
