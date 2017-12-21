using FluentAssertions;
using HealthAndCareHospital.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HealthAndCareHospital.Test.Services
{
    public class DoctorServiceTest
    {
        [Fact]
        public async Task CreateAsyncShouldReturnTrueAndNewDoctor()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho","email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();

            db.Doctors.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task EditShouldReturnTrueAndProperCountOfDoctors()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho", "email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();
            var id = await db.Doctors
                .Where(d => d.Name == "Gosho")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var edited = await doctorService.Edit(
                id, "GoshoEdit", "email@email.bg", "SomeEditURL", "CardiologistEdit", "Cardiology");
            await db.SaveChangesAsync();

            edited.Should()
                .BeTrue();

            db.Doctors.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task DeleteShouldReturnTrueAndProperCountOfDoctors()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho", "email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();
            var id = await db.Doctors
                .Where(d => d.Name == "Gosho")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var deleted = await doctorService.Delete(id);
            await db.SaveChangesAsync();

            deleted.Should()
                .BeTrue();

            db.Doctors.Should()
                .HaveCount(1);
        }
    }
}
