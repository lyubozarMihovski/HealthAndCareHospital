namespace HealthAndCareHospital.Web.Areas.Admin.Models
{
    using HealthAndCareHospital.Services.Models.Admin;

    public class DepartmentViewModel : DepartmentCreateServiceModel
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public List<DoctorsViewModel> Doctors { get; set; }
    }
}
