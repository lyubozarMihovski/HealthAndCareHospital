namespace HealthAndCareHospital.Services
{
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using HealthAndCareHospital.Services.Models.Doctor;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReceiptService
    {
        Task<bool> AppointmentExists(int id);
        Task<IEnumerable<ReceiptServiceModel>> All(string email);
        Task<IEnumerable<ReceiptServiceModel>> All();
        Task<ReceiptServiceModel> Details(int id);
        Task<bool> Create(string name, DateTime dateTime, string email);
        Task<bool> Delete(int id);
    }
}
