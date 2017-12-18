namespace HealthAndCareHospital.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MedicineService : IMedicineService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public MedicineService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<MedicineServiceModel>> All(int page = 1, int pageSize = 20)
        {
            return await this.db.Medicines
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<MedicineServiceModel>()
                .ToListAsync();
        }

        public async Task Create(string name, string dosage, string descritption)
        {
            var medicine = new Medicine
            {
                Name = name,
                Dosage = dosage,
                Descritption = descritption
            };

            this.db.Medicines.Add(medicine);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            if (medicine == null)
            {
                return false;
            }

            this.db.Remove(medicine);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<MedicineServiceModel> Details(int id)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .ProjectTo<MedicineServiceModel>()
                .FirstOrDefaultAsync();

            return medicine;
        }

        public async Task<bool> Edit(int id,string name, string dosage, string descritption)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            if (medicine == null)
            {
                return false;
            }

            medicine.Name = name;
            medicine.Dosage = dosage;
            medicine.Descritption = descritption;

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MedicineExists(int id)
        {
            return await this.db.Medicines.AnyAsync(e => e.Id == id);
        }

        public async Task<int> Total()
        {
            return await this.db.Medicines.CountAsync();
        }
    }
}
