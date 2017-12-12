namespace HealthAndCareHospital.Services.Models.Doctor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReceiptServiceModel
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        [Display(Name = "Appointment date and hour")]
        public DateTime DateTime { get; set; }

        public int DoctorId { get; set; }
    }
}
