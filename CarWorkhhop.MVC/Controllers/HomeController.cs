using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CarWorkhhop.MVC.Models;

namespace CarWorkhhop.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NoAccess()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();//wyświetla zawartość widoku w Views/Home/Privacy.cshtml
    }

    public IActionResult About()
    {
        //tworzymy model na podstawie klasy w folderze model i przekazujemy go do widoku przez parametr
        var model = new About()
        {
            Title = "CarWorkshop Application",
            Description = "Some description",
            Tags = new List<string> { "car", "app", "free" }
        };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
