namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDepartmentService
    {
        Task CreateAsync(string name, string description, string imageURL);
        Task<IEnumerable<DepartmentViewModel>> All();
    }
}
