namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMedicineService
    {
        Task<IEnumerable<MedicineServiceModel>> All();
        Task Create(string name, string dosage, string descritption);
        Task<bool> MedicineExists(int id);
        Task<MedicineServiceModel> Details(int id);
        Task Edit(int id,string name, string dosage, string descritption);
        Task Delete(int id);
    }
}
