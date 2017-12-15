namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDepartmentService
    {
        Task<bool> CreateAsync(string name, string description, string imageURL);
        IEnumerable<DepartmentViewModel> All();
        Task<bool> DepartmentExists(int id);
        Task<DepartmentCreateServiceModel> Details(int departmentId);
        Task<bool> Delete(int id);
        Task<bool> Edit(int id, string name, string description, string imageURL);
        Task<Department> FindByName(string departmentName);
    }
}
