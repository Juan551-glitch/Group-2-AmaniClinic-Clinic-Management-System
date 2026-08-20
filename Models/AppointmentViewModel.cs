using System.ComponentModel.DataAnnotations;

namespace AmaniClinic.Models;

public class AppointmentViewModel
{
    [Required(ErrorMessage = "Please enter your full name.")]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a phone number.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a doctor.")]
    public string Doctor { get; set; } = string.Empty;

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
