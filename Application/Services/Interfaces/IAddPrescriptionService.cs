using assignment_nine.Hospital.Application.DTOs;

namespace assignment_nine.Hospital.Application.Services.Interfaces
{
    public interface IAddPrescriptionService
    {
        Task AddPrescription(AddPrescriptionRequest req);
    }
}
