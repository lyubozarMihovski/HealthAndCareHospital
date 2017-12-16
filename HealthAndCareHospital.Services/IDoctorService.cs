namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Doctor;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDoctorService
    {
        Task<bool> CreateAsync(string name, string email, string imageURL, string speciality, string departmentName);
        Task<IEnumerable<DoctorViewModel>> All();
        Task<Doctor> GetDoctorById(int id);
        Task<DoctorViewModel> Details(int doctorId);
        Task<bool> Edit(int id, string name, string email, string imageURL, string speciality, string departmentName);
        Task<bool> DoctorExists(int id);
        Task<bool> Delete(int id);
        Task<bool> SetToRoleAsync(int id);
    }
}
