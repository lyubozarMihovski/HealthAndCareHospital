namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDepartmentService
    {
        Task CreateAsync(string name, string description, string imageURL);
        IEnumerable<DepartmentViewModel> All();
        Task<bool> DepartmentExists(int id);
        Task<DepartmentCreateServiceModel> Details(int departmentId);
        Task Delete(int id);
        Task Edit(int id, string name, string description, string imageURL);
        Task<Department> FindByName(string departmentName);
    }
}
