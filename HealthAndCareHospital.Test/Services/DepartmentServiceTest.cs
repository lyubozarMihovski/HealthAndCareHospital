namespace HealthAndCareHospital.Test.Services
{
    using FluentAssertions;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class DepartmentServiceTest
    {
        [Fact]
        public async Task CreateAsyncShouldReturnNewDepartment()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();

            db.Departments.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task EditShouldReturnTrueAndProperCountOfDepartmentst()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var id = await db.Departments
                .Where(d => d.Name == "Department")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var edited = await departmentService.Edit(id, "EditedDepartment", "Gosho", "SomeURL");
            await db.SaveChangesAsync();

            edited.Should()
                .BeTrue();

            db.Departments.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task DeleteShouldReturnTrueAndProperCountOfDepartments()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var id = await db.Departments
                .Where(d => d.Name == "Department")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var deleted = await departmentService.Delete(id);
            await db.SaveChangesAsync();

            deleted.Should()
                .BeTrue();

            db.Departments.Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task FindByNameShouldReturnTrueDepartment()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var finded = await departmentService.FindByName("Department");

            finded.Should().BeOfType<Department>();
        }
    }
}
