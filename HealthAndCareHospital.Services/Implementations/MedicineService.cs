﻿namespace HealthAndCareHospital.Services.Implementations
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

        public async Task<IEnumerable<MedicineServiceModel>> All()
        {
            return await this.db.Medicines
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

        public async Task Delete(int id)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            this.db.Remove(medicine);
            await this.db.SaveChangesAsync();
        }

        public async Task<MedicineServiceModel> Details(int id)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .ProjectTo<MedicineServiceModel>()
                .FirstOrDefaultAsync();

            return medicine;
        }

        public async Task Edit(int id,string name, string dosage, string descritption)
        {
            var medicine = await this.db.Medicines
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();

            medicine.Name = name;
            medicine.Dosage = dosage;
            medicine.Descritption = descritption;

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> MedicineExists(int id)
        {
            return await this.db.Medicines.AnyAsync(e => e.Id == id);
        }
    }
}