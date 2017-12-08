namespace HealthAndCareHospital.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Disease
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

        public Department Department { get; set; }
    }
}
