using HealthAndCareHospital.Data;
using HealthAndCareHospital.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HealthAndCareHospital.Services.Models.Doctor;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace HealthAndCareHospital.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public DoctorService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DoctorViewModel>> All()
        {
            var doctors = await this.db.Doctors
                .ProjectTo<DoctorViewModel>()
                .ToListAsync();

            return doctors;
        }

        public async Task CreateAsync(string name, string email, string imageURL, string speciality, string departmentName)
        {
            var department = await this.db.Departments
                   .Where(d => d.Name == departmentName)
                   .FirstOrDefaultAsync();

            var doctor = new Doctor
            {
                Name = name,
                Email = email,
                ImageURL = imageURL,
                Speciality = speciality,
                Department = department
            };
            var user = await this.db.Users
                .Where(u => u.Email == doctor.Email)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            db.Add(doctor);
            db.Departments
                .Where(d => d.Id == department.Id)
                .FirstOrDefault()
                .Doctors
                .Add(doctor);
            await db.SaveChangesAsync();
        }
    }
}
