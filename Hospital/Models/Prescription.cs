using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_nine.Hospital.Models
{
    public partial class Prescription
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }

        public int IdPatient { get; set; }
        public virtual Patient IdPatientNav { get; set; } = null!;
        public int IdDoctor { get; set; }
        public virtual Doctor IdDoctorNav { get; set; } = null!;

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } =
            new List<PrescriptionMedicament>();
    }
}
