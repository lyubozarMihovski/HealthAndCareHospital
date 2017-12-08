namespace HealthAndCareHospital.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Doctor
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

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public List<Patient> Patients { get; set; } = new List<Patient>();

        public List<Receipt> Receipts { get; set; } = new List<Receipt>();
    }
}
