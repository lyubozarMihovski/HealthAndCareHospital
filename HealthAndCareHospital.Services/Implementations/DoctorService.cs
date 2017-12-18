namespace HealthAndCareHospital.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DoctorService : IDoctorService
    {
        private readonly HealthAndCareHospitalDbContext db;
        private UserManager<User> userManager;

        public DoctorService(HealthAndCareHospitalDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<DoctorViewModel>> All()
        {
            var doctors = await this.db.Doctors
                .ProjectTo<DoctorViewModel>()
                .ToListAsync();

            return doctors;
        }

        public async Task CreateAsync(string name,
            string email,
            string imageURL,
            string speciality,
            string departmentName)
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

            db.Add(doctor);
            db.Departments
                .Where(d => d.Id == department.Id)
                .FirstOrDefault()
                .Doctors
                .Add(doctor);

            await db.SaveChangesAsync();
        }

        public async Task<DoctorViewModel> Details(int doctorId)
        {
            var doc = await this.db.Doctors
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    DepartmentName = d.Department.Name,
                    Email = d.Email,
                    ImageURL = d.ImageURL,
                    Speciality = d.Speciality
                })
                .SingleOrDefaultAsync();

            return doc;
        }

        public async Task<bool> Edit(int id,
            string name,
            string email,
            string imageURL,
            string speciality,
            string departmentName)
        {
           var doctor = await this.db.Doctors
                    .Where(d => d.Id == id)
                    .SingleOrDefaultAsync();

               doctor.Name = name;
               doctor.Email = email;
               doctor.ImageURL = imageURL;
               doctor.Speciality = speciality;
               doctor.Department = await this.db.Departments
                     .Where(d => d.Name == departmentName)
                     .FirstOrDefaultAsync();
            if (doctor == null)
            {
                return false;
            }

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<Doctor> GetDoctorById(int id)
        {
            var doctor = await this.db.Doctors.FindAsync(id);
            return doctor;
        }

        public async Task<bool> DoctorExists(int id)
        {
            return await this.db.Doctors.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var doctor = await this.db.Doctors
                .SingleOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return false;
            }

            this.db.Doctors.Remove(doctor);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetToRoleAsync(int id)
        {
            var doctorUser = await this.GetDoctorById(id);
            var user = await this.db.Users
                .Where(u => u.Email == doctorUser.Email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(user, "Doctor");
            return true;
        }
    }
}
