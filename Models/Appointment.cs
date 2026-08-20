using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    [DataType(DataType.Date)] public DateTime AppointmentDate { get; set; }
    public TimeOnly AppointmentTime { get; set; }
    [StringLength(250)] public string? Reason { get; set; }
    [StringLength(30)] public string Status { get; set; } = "Scheduled";
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public ICollection<Consultation> Consultations { get; set; } = [];
}
