namespace HealthAndCareHospital.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using HealthAndCareHospital.Services.Models.Doctor;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DepartmentService : IDepartmentService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public DepartmentService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DepartmentViewModel> All()
        {
            return this.db.Departments.Select(d =>  new DepartmentViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageURL = d.ImageURL,
                Doctors = this.db.Doctors
                .Where(doc => doc.DepartmentId == d.Id)
                .Select(doc => new DoctorIdNameModel
                {
                    Id = doc.Id,
                    Name = doc.Name
                })
                .ToList()
            })
            .ToList();
        }

        public async Task CreateAsync(string name, string description, string imageURL)
        {
           var department = new Department
            {
                Name = name,
                Description = description,
                ImageURL = imageURL
            };

            this.db.Add(department);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var department = await this.db.Departments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return false;
            }

            this.db.Departments.Remove(department);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DepartmentExists(int id)
        {
            return await this.db.Departments.AnyAsync(e => e.Id == id);
        }

        public async Task<DepartmentCreateServiceModel> Details(int departmentId)
        {
            return await this.db.Departments
                 .Where(d => d.Id == departmentId)
                 .ProjectTo<DepartmentCreateServiceModel>()
                 .FirstOrDefaultAsync();
        }

        public async Task<bool> Edit(int id, string name, string description, string imageURL)
        {
            var department = await this.db.Departments
                 .Where(d => d.Id == id).FirstOrDefaultAsync();
            if (department == null)
            {
                return false;
            }

            department.Name = name;
            department.Description = description;
            department.ImageURL = imageURL;

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<Department> FindByName(string departmentName)
        {
            return await this.db
                .Departments
                .Where(d => d.Name.ToLower() == departmentName.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}
