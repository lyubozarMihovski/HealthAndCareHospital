namespace HealthAndCareHospital.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Department
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

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();

        public List<Disease> Diseases { get; set; } = new List<Disease>();
    }
}
