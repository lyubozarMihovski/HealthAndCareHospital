namespace HealthAndCareHospital.Services.Models.Admin
{
    using HealthAndCareHospital.Common.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class MedicineServiceModel : IMapFrom<Medicine>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.MedicineNameMinLength)]
        [MaxLength(DataConstants.MedicineNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.MedicineDosageMinLength)]
        [MaxLength(DataConstants.MedicineDosageMaxLength)]
        public string Dosage { get; set; }

        [Required]
        [MinLength(DataConstants.MedicineDescriptionMinLength)]
        [MaxLength(DataConstants.MedicineDescriptionMaxLength)]
        public string Descritption { get; set; }
    }
}
