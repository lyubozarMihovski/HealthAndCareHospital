namespace HealthAndCareHospital.Test.Services
{
    using FluentAssertions;
    using HealthAndCareHospital.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class PatientServiceTest
    {
        [Fact]
        public async Task CreateShouldReturnTrueAndNewPatient()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho", "email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();
            var patientService = new PatientService(db);
            var result = await patientService.Create("Patient",
                "8698969896",
                28, "email@email.bg");
            await db.SaveChangesAsync();

            result.Should()
                .BeTrue();

            db.Patients.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task DeleteShouldReturnTrueAndDeletedPatient()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho", "email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();
            var patientService = new PatientService(db);
            var result = await patientService.Create("Patient",
                "8698969896",
                28, "email@email.bg");
            await db.SaveChangesAsync();

            var id = await db.Patients
                .Where(d => d.Name == "Patient")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();

            var deleted = await patientService.Delete(id);
            await db.SaveChangesAsync();

            result.Should()
                .BeTrue();

            deleted.Should()
                .BeTrue();

            db.Patients.Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task EditShouldReturnTrueAndEditedPatient()
        {
            var db = Tests.GetDatabase();
            var doctorService = new DoctorService(db, null);
            var departmentService = new DepartmentService(db);
            await departmentService.CreateAsync("Cardiology", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            await doctorService.CreateAsync(
                "Gosho", "email@email.bg", "SomeURL", "Cardiologist", "Cardiology");
            await db.SaveChangesAsync();
            var patientService = new PatientService(db);
            var result = await patientService.Create("Patient",
                "8698969896",
                28, "email@email.bg");
            await db.SaveChangesAsync();

            var id = await db.Patients
                .Where(d => d.Name == "Patient")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();

            var edited = await patientService
                .Edit(id, "PatientEdited",
                "8698969896",
                28, "email@email.bg");

            edited.Should()
                .BeTrue();

            db.Patients.Should()
                .HaveCount(1);

            result.Should()
                .BeTrue();
        }
    }
}
