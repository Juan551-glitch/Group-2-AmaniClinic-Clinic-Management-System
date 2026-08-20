using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    [Required, StringLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(50)] public string LastName { get; set; } = string.Empty;
    [StringLength(100)] public string? Specialization { get; set; }
    [StringLength(20)] public string? Phone { get; set; }
    [EmailAddress, StringLength(100)] public string? Email { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
    public string DisplayName => $"Dr. {FirstName} {LastName}";
}
