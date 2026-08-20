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

        var recentAppointments = await AppointmentDetails()
            .Take(10)
            .ToListAsync();

        return View(recentAppointments);
    }

    public async Task<IActionResult> Patients()
    {
        var patients = await context.Patients
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return View(patients);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePatient(Patient patient)
    {
        if (!ModelState.IsValid)
        {
            return await Patients();
        }

        context.Patients.Add(patient);
        await context.SaveChangesAsync();

        TempData["Message"] = "Patient added.";
        return RedirectToAction(nameof(Patients));
    }

    public async Task<IActionResult> Doctors()
    {
        var doctors = await context.Doctors
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return View(doctors);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDoctor(Doctor doctor)
    {
        if (!ModelState.IsValid)
        {
            return await Doctors();
        }

        context.Doctors.Add(doctor);
        await context.SaveChangesAsync();

        TempData["Message"] = "Doctor added.";
        return RedirectToAction(nameof(Doctors));
    }

    public async Task<IActionResult> Appointments()
    {
        var appointments = await AppointmentDetails().ToListAsync();
        return View(appointments);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateAppointmentStatus(int id, string status)
    {
        var appointment = await context.Appointments.FindAsync(id);
        var validStatuses = new[] { "Scheduled", "Completed", "Cancelled" };

        if (appointment is not null && validStatuses.Contains(status))
        {
            appointment.Status = status;
            await context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Appointments));
    }

    public async Task<IActionResult> Consultations()
    {
        ViewBag.Appointments = await AppointmentDetails().ToListAsync();

        var consultations = await context.Consultations
            .Include(x => x.Appointment)
            .ThenInclude(x => x.Patient)
            .OrderByDescending(x => x.ConsultationDate)
            .ToListAsync();

        return View(consultations);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateConsultation(Consultation consultation)
    {
        if (ModelState.IsValid)
        {
            context.Consultations.Add(consultation);
            await context.SaveChangesAsync();

            TempData["Message"] = "Consultation recorded.";
        }

        return RedirectToAction(nameof(Consultations));
    }

    private IQueryable<Appointment> AppointmentDetails() => context.Appointments
        .Include(x => x.Patient)
        .Include(x => x.Doctor)
        .OrderByDescending(x => x.AppointmentDate)
        .ThenBy(x => x.AppointmentTime);
}
