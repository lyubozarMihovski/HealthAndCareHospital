namespace HealthAndCareHospital.Services.Implementations
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

    public class DiseaseService : IDiseaseService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public DiseaseService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DiseaseServiceModel>> All()
        {
            return await this.db.Diseases.Select(d => new DiseaseServiceModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                DepartmentId = d.DepartmentId,
                DepartmentName = d.Department.Name
            })
            .ToListAsync();
        }

        public async Task Create(string name, string description, Department department)
        {
            var disease = new Disease
            {
                Name = name,
                Description = description,
                DepartmentId = department.Id,
                Department = department
            };

            department.Diseases.Add(disease);
            this.db.Diseases.Add(disease);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var disease = await db.Diseases
                .SingleOrDefaultAsync(m => m.Id == id);

            this.db.Diseases.Remove(disease);
            await db.SaveChangesAsync();
        }

        public async Task<DiseaseServiceModel> Details(int id)
        {
            var diseaseModel = await this.db.Diseases.Select(d => new DiseaseServiceModel
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                DepartmentId = d.DepartmentId,
                DepartmentName = d.Department.Name
            })
            .FirstOrDefaultAsync();

            return diseaseModel;
        }

        public async Task<bool> DiseaseExists(int id)
        {
            return await this.db.Diseases.AnyAsync(e => e.Id == id);
        }

        public async Task Edit(int id, string name, string description, string departmentName)
        {
            var disease = await this.db.Diseases.FindAsync(id);

            disease.Name = name;
            disease.DepartmentId = await this.db.Departments
                .Where(d => d.Name == departmentName)
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            disease.Description = description;

            await this.db.SaveChangesAsync();
        }

        public async Task<Disease> FindById(int id)
        {
            return await this.db.Diseases
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DiseaseServiceModel>> Search(DiseaseListingModel model)
        {
            return await this.db.Diseases
                .OrderBy(d => d.Name)
                .Where(d => d.Name.ToLower()
                .Contains(model.SearchText.ToLower()))
                .Select(d => new DiseaseServiceModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.Department.Name
                })
                .ToListAsync();
        }
    }
}
