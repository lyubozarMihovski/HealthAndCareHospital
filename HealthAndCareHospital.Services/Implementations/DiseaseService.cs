namespace HealthAndCareHospital.Services.Implementations
{
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<bool> Delete(int id)
        {
            var disease = await db.Diseases
                .SingleOrDefaultAsync(m => m.Id == id);
            if (disease == null)
            {
                return false;
            }

            this.db.Diseases.Remove(disease);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<DiseaseServiceModel> Details(int id)
        {
            return await this.db.Diseases.Select(d => new DiseaseServiceModel
               {
                   Id = d.Id,
                   Name = d.Name,
                   Description = d.Description,
                   DepartmentId = d.DepartmentId,
                   DepartmentName = d.Department.Name
               })
               .FirstOrDefaultAsync();
        }

        public async Task<bool> DiseaseExists(int id)
        {
            return await this.db.Diseases.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> Edit(int id, string name, string description, string departmentName)
        {
            var disease = await this.db.Diseases.FindAsync(id);
            if (disease == null)
            {
                return false;
            }

            disease.Name = name;
            disease.DepartmentId = await this.db.Departments
                .Where(d => d.Name == departmentName)
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            disease.Description = description;

            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<Disease> FindById(int id)
        {
            return await this.db.Diseases
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DiseaseServiceModel>> Search(string searchText)
        {
            return await this.db.Diseases
                .OrderBy(d => d.Name)
                .Where(d => d.Name.ToLower()
                .Contains(searchText.ToLower()))
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
