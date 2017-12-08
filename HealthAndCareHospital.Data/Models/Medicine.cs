namespace HealthAndCareHospital.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Medicine
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

        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }
    }
}
