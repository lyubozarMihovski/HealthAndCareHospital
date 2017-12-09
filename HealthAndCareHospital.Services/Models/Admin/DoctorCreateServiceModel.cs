namespace HealthAndCareHospital.Services.Models.Admin
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class DoctorCreateServiceModel : IMapFrom<Doctor>
    {
        [Required]
        [MinLength(DataConstants.DoctorNameMinLength)]
        [MaxLength(DataConstants.DoctorNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(DataConstants.DoctorSpecialityMinLength)]
        [MaxLength(DataConstants.DoctorSpecialityMaxLength)]
        public string Speciality { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }
    }
}
