using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class Consultation
{
    public int ConsultationId { get; set; }
    public int AppointmentId { get; set; }
    [DataType(DataType.Date)] public DateTime ConsultationDate { get; set; }
    [StringLength(500)] public string? Symptoms { get; set; }
    [StringLength(500)] public string? Diagnosis { get; set; }
    [StringLength(500)] public string? Treatment { get; set; }
    [StringLength(1000)] public string? Notes { get; set; }
    public Appointment Appointment { get; set; } = null!;
}
