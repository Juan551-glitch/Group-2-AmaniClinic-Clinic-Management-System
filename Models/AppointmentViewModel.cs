using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class AppointmentViewModel
{
    [Required(ErrorMessage = "Please enter your first name.")]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your last name.")]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your date of birth.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date of birth")]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a phone number.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a doctor.")]
    [Display(Name = "Doctor")]
    public int? DoctorId { get; set; }

    public IEnumerable<Doctor> Doctors { get; set; } = [];

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    [Display(Name = "Preferred date")]
    public DateTime? PreferredDate { get; set; }

    [Required(ErrorMessage = "Please choose a time.")]
    [Display(Name = "Preferred time")]
    public string PreferredTime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please tell us why you are visiting.")]
    [StringLength(500)]
    [Display(Name = "Reason for visit")]
    public string ReasonForVisit { get; set; } = string.Empty;
}
