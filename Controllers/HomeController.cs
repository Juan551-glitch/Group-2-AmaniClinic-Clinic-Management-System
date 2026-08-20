using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AmaniClinic.Models;

namespace AmaniClinic.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Appointment()
    {
        return View(new AppointmentViewModel { PreferredDate = DateTime.Today.AddDays(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Appointment(AppointmentViewModel appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        return View("AppointmentConfirmation", appointment);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
