using AmaniClinic.Data;
using AmaniClinic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmaniClinic.Controllers;

public class ClinicController(AmaniClinicContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.PatientCount = await context.Patients.CountAsync();
        ViewBag.DoctorCount = await context.Doctors.CountAsync();
        ViewBag.AppointmentCount = await context.Appointments.CountAsync();
        ViewBag.ConsultationCount = await context.Consultations.CountAsync();
        return View(await context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
            .OrderByDescending(x => x.AppointmentDate).ThenBy(x => x.AppointmentTime).Take(10).ToListAsync());
    }

    public async Task<IActionResult> Patients() => View(await context.Patients.OrderBy(x => x.LastName).ToListAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePatient(Patient patient)
    {
        if (!ModelState.IsValid) return View("Patients", await context.Patients.OrderBy(x => x.LastName).ToListAsync());
        context.Patients.Add(patient); await context.SaveChangesAsync();
        TempData["Message"] = "Patient added."; return RedirectToAction(nameof(Patients));
    }

    public async Task<IActionResult> Doctors() => View(await context.Doctors.OrderBy(x => x.LastName).ToListAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDoctor(Doctor doctor)
    {
        if (!ModelState.IsValid) return View("Doctors", await context.Doctors.OrderBy(x => x.LastName).ToListAsync());
        context.Doctors.Add(doctor); await context.SaveChangesAsync();
        TempData["Message"] = "Doctor added."; return RedirectToAction(nameof(Doctors));
    }

    public async Task<IActionResult> Appointments() => View(await context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
        .OrderByDescending(x => x.AppointmentDate).ThenBy(x => x.AppointmentTime).ToListAsync());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAppointmentStatus(int id, string status)
    {
        var appointment = await context.Appointments.FindAsync(id);
        if (appointment is not null && new[] { "Scheduled", "Completed", "Cancelled" }.Contains(status))
        {
            appointment.Status = status; await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Appointments));
    }

    public async Task<IActionResult> Consultations()
    {
        ViewBag.Appointments = await context.Appointments.Include(x => x.Patient).Include(x => x.Doctor)
            .OrderByDescending(x => x.AppointmentDate).ToListAsync();
        return View(await context.Consultations.Include(x => x.Appointment).ThenInclude(x => x.Patient)
            .OrderByDescending(x => x.ConsultationDate).ToListAsync());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConsultation(Consultation consultation)
    {
        if (ModelState.IsValid) { context.Consultations.Add(consultation); await context.SaveChangesAsync(); TempData["Message"] = "Consultation recorded."; }
        return RedirectToAction(nameof(Consultations));
    }
}
