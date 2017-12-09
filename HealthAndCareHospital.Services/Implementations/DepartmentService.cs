using HealthAndCareHospital.Data;
using HealthAndCareHospital.Data.Models;
using System.Threading.Tasks;
using HealthAndCareHospital.Services.Models.Admin;
using System.Collections.Generic;
using System.Linq;
using HealthAndCareHospital.Services.Models.Doctor;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace HealthAndCareHospital.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public DepartmentService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DepartmentViewModel>> All()
        {
            var departments = await this.db.Departments.Select(d =>  new DepartmentViewModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                ImageURL = d.ImageURL,
                Doctors = this.db.Doctors
                .Where(doc => doc.DepartmentId == d.Id)
                .ProjectTo<DoctorViewModel>()
                .ToList()
            })
              .ToListAsync();
            return departments;
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
    }
}
