using System.ComponentModel.DataAnnotations;
using assignment_nine.Hospital.Application.DTOs;
using assignment_nine.Hospital.Application.Repos;
using assignment_nine.Hospital.Application.Services.Interfaces;
using assignment_nine.Hospital.Models;

namespace assignment_nine.Hospital.Application.Services;

public class AddPrescriptionService : IAddPrescriptionService
{
    private readonly IAddPrescriptionRepo _addPrescriptionRepository;

    public AddPrescriptionService(IAddPrescriptionRepo addPrescriptionRepository)
    {
        _addPrescriptionRepository = addPrescriptionRepository;
    }

    // Service does the validations lol

    public async Task AddPrescription(AddPrescriptionRequest req)
    {
        if (!await _addPrescriptionRepository.IsDoctorAvailable(req.Doctor.IdDoctor))
        {
            throw new ValidationException("You can only add 10 Medicaments per request.");
        }
        Patient patient = new Patient
        {
            IdPatient = req.Patient.IdPatient,
            FirstName = req.Patient.FirstName,
            LastName = req.Patient.LastName,
            BirthDate = req.Patient.BirthDate
        };

        if (await _addPrescriptionRepository.ShouldCreatePatientAsync(patient.IdPatient))
        {
            await _addPrescriptionRepository.CreateAndInsertPatient(patient);
        }

        if (req.Medicaments.Count > 10)
        {
            throw new ValidationException("You can only add 10 Medicaments per request.");
        }

        foreach (var med in req.Medicaments)
        {
            if (!await _addPrescriptionRepository.IsMedicamentInPrescription(med.IdMedicament))
            {
                throw new ValidationException(
                    $"Medicament with ID {med.IdMedicament} listed in the prescription does not exist."
                );
            }
        }

        if (req.DueDate.Date >= req.Date.Date)
        {
            throw new ValidationException(
                $"Due date ({req.DueDate.Date}) is greater than Date ({req.Date.Date})"
            );
        }

        await _addPrescriptionRepository.AddPrescription(req);
    }
}
