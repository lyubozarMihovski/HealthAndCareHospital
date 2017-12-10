namespace HealthAndCareHospital.Services.Models.Admin
{
    using AutoMapper;
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class DiseaseServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.DiseaseNameMinLength)]
        [MaxLength(DataConstants.DiseaseNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.DiseaseDescriptionMinLength)]
        [MaxLength(DataConstants.DiseaseDescriptionMaxLength)]
        public string Description { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
