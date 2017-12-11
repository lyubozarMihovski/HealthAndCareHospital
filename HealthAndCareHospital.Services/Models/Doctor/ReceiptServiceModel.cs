namespace HealthAndCareHospital.Services.Models.Doctor
{
    using System;

    public class ReceiptServiceModel
    {
        public int Id { get; set; }

        public string PatientName { get; set; }

        public DateTime DateTime { get; set; }

        public int DoctorId { get; set; }
    }
}
