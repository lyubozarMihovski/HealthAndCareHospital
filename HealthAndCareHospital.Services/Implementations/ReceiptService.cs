namespace HealthAndCareHospital.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReceiptService : IReceiptService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public ReceiptService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ReceiptServiceModel>> All(string email)
        {
            return await this.db.Receipts
               .Where(r => r.Doctor.Email == email)
               .OrderByDescending(r => r.DateTime)
               .ProjectTo<ReceiptServiceModel>()
               .ToListAsync();
        }

        public async Task<IEnumerable<ReceiptServiceModel>> All()
        {
            return await this.db.Receipts
               .OrderByDescending(r => r.DateTime)
               .ProjectTo<ReceiptServiceModel>()
               .ToListAsync();
        }

        public async Task<bool> Create(string name, DateTime dateTime, string email)
        {
            var doctor = await this.db.Doctors
                .Where(d => d.Email == email)
                .FirstOrDefaultAsync();

            if (doctor == null)
            {
                return false;
            }

            var receipt = new Receipt
            {
                PatientName = name,
                DateTime = dateTime,
                Doctor = doctor,
                DoctorId = doctor.Id
            };

            if (receipt == null)
            {
                return false;
            }

            this.db.Receipts.Add(receipt);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var receipt = await this.db.Receipts
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (receipt == null)
            {
                return false;
            }

            this.db.Receipts.Remove(receipt);
            await this.db.SaveChangesAsync();
            return true;

        }

        public async Task<ReceiptServiceModel> Details(int id)
        {
            return await this.db.Receipts
               .Where(r => r.Id == id)
               .Select(r => new ReceiptServiceModel
               {
                   Id = r.Id,
                   PatientName = r.PatientName,
                   DateTime = r.DateTime,
                   DoctorId = r.DoctorId
               })
               .FirstOrDefaultAsync();
        }

        public async Task<bool> AppointmentExists(int id)
        {
            return await this.db.Receipts.AnyAsync(e => e.Id == id);
        }
    }
}
