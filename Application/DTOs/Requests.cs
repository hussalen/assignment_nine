using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_nine.Hospital.Application.DTOs
{
    public record AddPrescriptionRequest(
        [Required] PatientDTO Patient,
        [Required] DoctorDTO Doctor,
        [Required] List<MedicamentsDTO> Medicaments,
        [Required] DateTime Date,
        [Required] DateTime DueDate
    );
}
