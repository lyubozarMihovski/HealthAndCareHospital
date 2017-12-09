namespace HealthAndCareHospital.Services.Models.Admin
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentCreateServiceModel : IMapFrom<Department>
    {
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
    }
}
