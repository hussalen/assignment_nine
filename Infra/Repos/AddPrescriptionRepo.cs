using assignment_nine.Hospital.Application.DTOs;
using assignment_nine.Hospital.Application.Repos;
using assignment_nine.Hospital.Infra;
using assignment_nine.Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace assignment_nine.Hospital.Infra.Repos;

public class AddPrescriptionRepo : IAddPrescriptionRepo
{
    private readonly HospitaldbContext _hospitalDbContext;

    public AddPrescriptionRepo(HospitaldbContext hospitalDbContext)
    {
        _hospitalDbContext = hospitalDbContext;
    }

    //This is where the queries happen.

    // If patient does not exist (query the db to check).......

    public async Task<Boolean> ShouldCreatePatientAsync(int idPatient)
    {
        return !await _hospitalDbContext
            .Patients.Where(e => e.IdPatient == idPatient)
            .Select(e => e)
            .AnyAsync();
    }

    // ...... then create new patient from request (query db).
    public async Task CreateAndInsertPatient(Patient reqPatient)
    {
        var patient = new Patient
        {
            IdPatient = reqPatient.IdPatient,
            FirstName = reqPatient.FirstName,
            LastName = reqPatient.LastName,
            Prescriptions = reqPatient.Prescriptions,
            BirthDate = reqPatient.BirthDate
        };

        _hospitalDbContext.Patients.Add(patient);
        await _hospitalDbContext.SaveChangesAsync();
    }

    // If doctor does not exist (query the db to check, then throw an error.
    public async Task<Boolean> IsDoctorAvailable(int idDoctor)
    {
        return !await _hospitalDbContext
            .Doctors.Where(e => e.IdDoctor == idDoctor)
            .Select(e => e)
            .AnyAsync();
    }

    private async Task<int> GetNewPrescriptionId()
    {
        int id = await _hospitalDbContext
            .Prescriptions.OrderByDescending(e => e.IdPrescription)
            .Select(e => e.IdPrescription)
            .FirstOrDefaultAsync();
        return id++;
    }

    public async Task<Boolean> IsMedicamentInPrescription(int idMedicament)
    {
        return !await _hospitalDbContext
            .PrescriptionMedicaments.Where(e => e.IdMedicament == idMedicament)
            .Select(e => e)
            .AnyAsync();
    }

    public async Task AddPrescription(AddPrescriptionRequest req)
    {
        int newPrescriptionId = await GetNewPrescriptionId();

        var prescription = new Prescription
        {
            IdPrescription = newPrescriptionId,
            Date = req.Date,
            DueDate = req.DueDate,
            IdPatient = req.Patient.IdPatient,
            IdDoctor = req.Doctor.IdDoctor,
        };

        foreach (var med in req.Medicaments)
        {
            await _hospitalDbContext.Medicaments.AddAsync(
                new Medicament
                {
                    IdMedicament = med.IdMedicament,
                    Name = "Medicament",
                    Description = med.Description,
                    Type = "Aspirin"
                }
            );
            await _hospitalDbContext.PrescriptionMedicaments.AddAsync(
                new PrescriptionMedicament
                {
                    IdMedicament = med.IdMedicament,
                    IdPrescription = newPrescriptionId,
                    Dose = med.Dose,
                    Details = med.Description
                }
            );
        }

        await _hospitalDbContext.Prescriptions.AddAsync(prescription);
        await _hospitalDbContext.SaveChangesAsync();
    }
}
