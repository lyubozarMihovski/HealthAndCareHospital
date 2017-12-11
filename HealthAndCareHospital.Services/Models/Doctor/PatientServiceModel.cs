namespace HealthAndCareHospital.Services.Models.Doctor
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class PatientServiceModel : IMapFrom<Patient>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.MedicineDescriptionMinLength)]
        [MaxLength(DataConstants.MedicineDescriptionMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string EGN { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }

        public int DoctorId { get; set; }
    }
}
