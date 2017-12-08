namespace HealthAndCareHospital.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Receipt
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public DateTime DateTime { get; set; }

        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}
