﻿namespace HealthAndCareHospital.Services.Implementations
{
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using System.Threading.Tasks;
    using HealthAndCareHospital.Services.Models.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using HealthAndCareHospital.Services.Models.Doctor;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    public class DepartmentService : IDepartmentService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public DepartmentService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<DepartmentViewModel> All()
        {
            var departments = this.db.Departments.Select(d =>  new DepartmentViewModel
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

        public async Task Delete(int id)
        {
           var department = await this.db.Departments
                .SingleOrDefaultAsync(m => m.Id == id);

            this.db.Departments.Remove(department);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DepartmentExists(int id)
        {
            return await this.db.Departments.AnyAsync(e => e.Id == id);
        }

        public async Task<DepartmentCreateServiceModel> Details(int departmentId)
        {
            var department = await this.db.Departments
                 .Where(d => d.Id == departmentId)
                 .ProjectTo<DepartmentCreateServiceModel>()
                 .FirstOrDefaultAsync();

            return department;
        }

        public async Task Edit(int id, string name, string description, string imageURL)
        {
            var department = await this.db.Departments
                 .Where(d => d.Id == id).FirstOrDefaultAsync();

            department.Name = name;
            department.Description = description;
            department.ImageURL = imageURL;

            await this.db.SaveChangesAsync();
        }

        public async Task<Department> FindByName(string departmentName)
        {
            return await this.db
                .Departments
                .Where(d => d.Name == departmentName.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}