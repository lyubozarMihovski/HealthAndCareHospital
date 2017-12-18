namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Doctor;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPatientService
    {
        Task<IEnumerable<PatientServiceModel>> All();
        Task<bool> PatientExists(int id);
        Task<PatientServiceModel> Details(int id);
        Task<bool> Create(string name, string EGN, int age, string email);
        Task<bool> Edit(int id, string name, string EGN, int age, string email);
        Task<bool> Delete(int id);
    }
}
