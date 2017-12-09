namespace HealthAndCareHospital.Services.Models.Admin
{
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Services.Models.Doctor;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.DepartmentNameMinLength)]
        [MaxLength(DataConstants.DepartmentNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.DepartmentNameMinLength)]
        [MaxLength(DataConstants.DepartmentDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageURL { get; set; }

        public List<DoctorViewModel> Doctors { get; set; }
    }
}
