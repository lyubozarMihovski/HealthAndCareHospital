using HealthAndCareHospital.Services.Models.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthAndCareHospital.Services
{
    public interface IDoctorService
    {
        Task CreateAsync(string name, string email, string imageURL, string speciality, string departmentName);
        Task<IEnumerable<DoctorViewModel>> All();
    }
}
