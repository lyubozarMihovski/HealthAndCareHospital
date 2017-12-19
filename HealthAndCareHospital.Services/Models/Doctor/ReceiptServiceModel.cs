namespace HealthAndCareHospital.Services.Models.Doctor
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ReceiptServiceModel : IMapFrom<Receipt>
    {
        public int Id { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Appointment date and hour")]
        public DateTime DateTime { get; set; }

        public int DoctorId { get; set; }
    }
}
