namespace HealthAndCareHospital.Services.Models.Doctor
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    public class DoctorViewModel : IMapFrom<Doctor>, IHaveCustomMapping
    {
        public int Id { get; set; }

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

        public string DepartmentName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Doctor, DoctorViewModel>()
                .ForMember(d => d.DepartmentName, cfg => cfg.MapFrom(dr => dr
                .Department.Name));
        }
    }
}
