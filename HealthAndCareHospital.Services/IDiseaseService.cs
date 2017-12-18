namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDiseaseService
    {
        Task<IEnumerable<DiseaseServiceModel>> All();
        Task<IEnumerable<DiseaseServiceModel>> Search(string searchText);
        Task<bool> DiseaseExists(int id);
        Task<Disease> FindById(int id);
        Task Create(string name, string description, Department department);
        Task<DiseaseServiceModel> Details(int id);
        Task<bool> Edit(int id, string name, string description, string departmentName);
        Task<bool> Delete(int id);
    }
}
