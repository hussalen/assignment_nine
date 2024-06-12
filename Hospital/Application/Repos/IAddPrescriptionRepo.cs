using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment_nine.Hospital.Application.DTOs;
using assignment_nine.Hospital.Models;

namespace assignment_nine.Hospital.Application.Repos
{
    public interface IAddPrescriptionRepo
    {
        Task<Boolean> ShouldCreatePatientAsync(int idPatient);

        Task CreateAndInsertPatient(Patient reqPatient);
        Task<Boolean> IsDoctorAvailable(int idDoctor);

        Task<Boolean> IsMedicamentInPrescription(int idMedicament);
        Task AddPrescription(AddPrescriptionRequest req);
    }
}
