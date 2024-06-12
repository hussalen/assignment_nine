using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_nine.Hospital.Application.DTOs
{
    public class PatientDTO
    {
        public required int IdPatient { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public required DateTime BirthDate { get; set; }
    }

    public class DoctorDTO
    {
        public required int IdDoctor { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
    }

    public class MedicamentsDTO
    {
        public required int IdMedicament { get; set; }
        public required int Dose { get; set; }
        public required string Description { get; set; } = string.Empty;
    }
}
