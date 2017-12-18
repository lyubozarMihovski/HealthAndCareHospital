namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMedicineService
    {
        Task<IEnumerable<MedicineServiceModel>> All(int page = 1, int pageSize = 20);
        Task Create(string name, string dosage, string descritption);
        Task<bool> MedicineExists(int id);
        Task<MedicineServiceModel> Details(int id);
        Task<bool> Edit(int id,string name, string dosage, string descritption);
        Task<bool> Delete(int id);
        Task<int> Total();
    }
}
