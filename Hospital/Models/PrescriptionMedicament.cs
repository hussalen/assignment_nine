using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment_nine.Hospital.Models
{
    public partial class PrescriptionMedicament
    {
        public int IdMedicament { get; set; } // Foreign key
        public virtual Medicament IdMedicamentNav { get; set; } = null!; // navigation property

        public int IdPrescription { get; set; } // Foriegn key
        public virtual Prescription IdPrescriptionNav { get; set; } = null!; // navigation property

        public int? Dose { get; set; }
        public string Details { get; set; }
    }
}
