namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Contact;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContactService
    {
        Task<IEnumerable<ContactFormServiceModel>> All();
        Task<Contact> FindByIdAsync(int id);
        Task CreateAsync(string name, string email, string subject, string message);
        Task DeleteAsync(int id);
        Task<ContactFormServiceModel> CreateModelAsync(int id);
    }
}
