using AmaniClinic.Models;
using Microsoft.EntityFrameworkCore;

namespace AmaniClinic.Data;

public class AmaniClinicContext(DbContextOptions<AmaniClinicContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Consultation> Consultations => Set<Consultation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().ToTable("Patients").HasKey(x => x.PatientId);
        modelBuilder.Entity<Patient>().Property(x => x.PatientId).HasColumnName("PatientID");
        modelBuilder.Entity<Patient>().Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Patient>().Property(x => x.LastName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Patient>().Property(x => x.DateOfBirth).HasColumnType("date").IsRequired();
        modelBuilder.Entity<Patient>().Property(x => x.Gender).HasMaxLength(20);
        modelBuilder.Entity<Patient>().Property(x => x.Phone).HasMaxLength(20);
        modelBuilder.Entity<Patient>().Property(x => x.Email).HasMaxLength(100);
        modelBuilder.Entity<Patient>().Property(x => x.Address).HasMaxLength(200);

        modelBuilder.Entity<Doctor>().ToTable("Doctors").HasKey(x => x.DoctorId);
        modelBuilder.Entity<Doctor>().Property(x => x.DoctorId).HasColumnName("DoctorID");
        modelBuilder.Entity<Doctor>().Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Doctor>().Property(x => x.LastName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<Doctor>().Property(x => x.Specialization).HasMaxLength(100);
        modelBuilder.Entity<Doctor>().Property(x => x.Phone).HasMaxLength(20);
        modelBuilder.Entity<Doctor>().Property(x => x.Email).HasMaxLength(100);

        modelBuilder.Entity<Appointment>().ToTable("Appointments").HasKey(x => x.AppointmentId);
        modelBuilder.Entity<Appointment>().Property(x => x.AppointmentId).HasColumnName("AppointmentID");
        modelBuilder.Entity<Appointment>().Property(x => x.PatientId).HasColumnName("PatientID");
        modelBuilder.Entity<Appointment>().Property(x => x.DoctorId).HasColumnName("DoctorID");
        modelBuilder.Entity<Appointment>().Property(x => x.AppointmentDate).HasColumnType("date");
        modelBuilder.Entity<Appointment>().Property(x => x.AppointmentTime).HasColumnType("time");
        modelBuilder.Entity<Appointment>().Property(x => x.Reason).HasMaxLength(250);
        modelBuilder.Entity<Appointment>().Property(x => x.Status).HasMaxLength(30).IsRequired().HasDefaultValue("Scheduled");
        modelBuilder.Entity<Appointment>().HasOne(x => x.Patient).WithMany(x => x.Appointments).HasForeignKey(x => x.PatientId);
        modelBuilder.Entity<Appointment>().HasOne(x => x.Doctor).WithMany(x => x.Appointments).HasForeignKey(x => x.DoctorId);

        modelBuilder.Entity<Consultation>().ToTable("Consultations").HasKey(x => x.ConsultationId);
        modelBuilder.Entity<Consultation>().Property(x => x.ConsultationId).HasColumnName("ConsultationID");
        modelBuilder.Entity<Consultation>().Property(x => x.AppointmentId).HasColumnName("AppointmentID");
        modelBuilder.Entity<Consultation>().Property(x => x.ConsultationDate).HasColumnType("date");
        modelBuilder.Entity<Consultation>().Property(x => x.Symptoms).HasMaxLength(500);
        modelBuilder.Entity<Consultation>().Property(x => x.Diagnosis).HasMaxLength(500);
        modelBuilder.Entity<Consultation>().Property(x => x.Treatment).HasMaxLength(500);
        modelBuilder.Entity<Consultation>().Property(x => x.Notes).HasMaxLength(1000);
        modelBuilder.Entity<Consultation>().HasOne(x => x.Appointment).WithMany(x => x.Consultations).HasForeignKey(x => x.AppointmentId);
    }
}
