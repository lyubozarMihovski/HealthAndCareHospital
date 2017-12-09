namespace HealthAndCareHospital.Services.Models.Contact
{
    using HealthAndCareHospital.Data;
    using System.ComponentModel.DataAnnotations;

    public class ContactFormServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ContactNameMinLength)]
        [MaxLength(DataConstants.ContactNameSubjectMaxLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(DataConstants.ContactNameSubjectMaxLength)]
        public string Subject { get; set; }

        [MaxLength(DataConstants.ContactNameSubjectMaxLength)]
        public string Message { get; set; }

        public bool IsSeen { get; set; } = false;
    }
}
