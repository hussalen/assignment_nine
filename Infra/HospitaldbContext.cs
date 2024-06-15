using System;
using System.Collections.Generic;
using assignment_nine.Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_nine.Hospital.Infra;

public partial class HospitaldbContext : DbContext
{
    private readonly string? _connectionString;

    public HospitaldbContext() { }

    public HospitaldbContext(
        IConfiguration configuration,
        DbContextOptions<HospitaldbContext> options
    )
        : base(options)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException(
                nameof(configuration),
                "Connection string is not set"
            );
    }

    public virtual DbSet<Medicament> Medicaments { get; set; }

    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer(_connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicament>(entity =>
        {
            entity.HasKey(e => e.IdMedicament).HasName("Medicament_pk");

            entity.ToTable("Medicament");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<PrescriptionMedicament>(entity =>
        {
            entity
                .HasKey(e => new { e.IdMedicament, e.IdPrescription })
                .HasName("Prescription_Medicament_pk");

            entity.ToTable("Prescription_Medicament");

            entity.Property(e => e.Dose).IsRequired(false);
            entity.Property(e => e.Details).HasMaxLength(100);

            entity
                .HasOne(d => d.IdMedicamentNav)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdMedicament)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Medicament");

            entity
                .HasOne(d => d.IdPrescriptionNav)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(d => d.IdPrescription)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Prescription");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
            entity.ToTable("Doctor");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.IdPrescription).HasName("Prescription_pk");

            entity.ToTable("Prescription");

            entity.Property(e => e.Date).HasColumnType("datetime2");
            entity.Property(e => e.DueDate).HasColumnType("datetime2");

            entity
                .HasOne(pr => pr.IdPatientNav)
                .WithMany(pat => pat.Prescriptions)
                .HasForeignKey(pr => pr.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity
                .HasOne(pr => pr.IdDoctorNav)
                .WithMany(doc => doc.Prescriptions)
                .HasForeignKey(pr => pr.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.IdPatient).HasName("Patient_pk");
            entity.ToTable("Patient");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.BirthDate).HasColumnType("datetime2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
