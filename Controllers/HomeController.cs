using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AmaniClinic.Models;
using AmaniClinic.Data;
using Microsoft.EntityFrameworkCore;

namespace AmaniClinic.Controllers;

public class HomeController(AmaniClinicContext context) : Controller
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
    public async Task<IActionResult> Appointment()
    {
        return View(new AppointmentViewModel
        {
            PreferredDate = DateTime.Today.AddDays(1),
            Doctors = await context.Doctors.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Appointment(AppointmentViewModel appointment)
    {
        if (!ModelState.IsValid)
        {
            appointment.Doctors = await context.Doctors.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync();
            return View(appointment);
        }

        var patient = await context.Patients.FirstOrDefaultAsync(x => x.Email == appointment.Email);
        if (patient is null)
        {
            patient = new Patient
            {
                FirstName = appointment.FirstName,
                LastName = appointment.LastName,
                DateOfBirth = appointment.DateOfBirth!.Value,
                Email = appointment.Email,
                Phone = appointment.PhoneNumber
            };
            context.Patients.Add(patient);
        }
        else
        {
            patient.FirstName = appointment.FirstName;
            patient.LastName = appointment.LastName;
            patient.DateOfBirth = appointment.DateOfBirth!.Value;
            patient.Phone = appointment.PhoneNumber;
        }

        var time = TimeOnly.Parse(appointment.PreferredTime[..5]);
        context.Appointments.Add(new Appointment
        {
            Patient = patient,
            DoctorId = appointment.DoctorId!.Value,
            AppointmentDate = appointment.PreferredDate!.Value,
            AppointmentTime = time,
            Reason = appointment.ReasonForVisit,
            Status = "Scheduled"
        });
        await context.SaveChangesAsync();
        return View("AppointmentConfirmation", appointment);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
