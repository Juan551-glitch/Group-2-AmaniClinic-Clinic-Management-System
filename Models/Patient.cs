using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class Patient
{
    public int PatientId { get; set; }
    [Required, StringLength(50)] public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(50)] public string LastName { get; set; } = string.Empty;
    [DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
    [StringLength(20)] public string? Gender { get; set; }
    [StringLength(20)] public string? Phone { get; set; }
    [EmailAddress, StringLength(100)] public string? Email { get; set; }
    [StringLength(200)] public string? Address { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = [];
}
