namespace HealthAndCareHospital.Test.Services
{
    using FluentAssertions;
    using HealthAndCareHospital.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class MedicineServiceTest
    {
        [Fact]
        public async Task CreateShouldReturnTrueAndNewMedicine()
        {
            var db = Tests.GetDatabase();
            var medicineService = new MedicineService(db);
            await medicineService.Create("Lekarstvo",
                "6 x 2",
                "Lekuva vsichkooooooooooooooooo");

            db.Medicines.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task DeleteShouldReturnTrueAndDeletedMedicine()
        {
            var db = Tests.GetDatabase();
            var medicineService = new MedicineService(db);
            await medicineService.Create("Lekarstvo",
                "6 x 2",
                "Lekuva vsichkooooooooooooooooo");
            await db.SaveChangesAsync();
            var id = await db.Medicines
                .Where(d => d.Name == "Lekarstvo")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var deleted = await medicineService.Delete(id);
            await db.SaveChangesAsync();

            deleted.Should()
                .BeTrue();

            db.Medicines.Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task EditShouldReturnTrueAndEditedMedicine()
        {
            var db = Tests.GetDatabase();
            var medicineService = new MedicineService(db);
            await medicineService.Create("Lekarstvo",
                "6 x 2",
                "Lekuva vsichkooooooooooooooooo");
            await db.SaveChangesAsync();
            var id = await db.Medicines
                .Where(d => d.Name == "Lekarstvo")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var edited = await medicineService
                .Edit(id, "EditedName", "6 x 1","Description");

            edited.Should()
                .BeTrue();

            db.Medicines.Should()
                .HaveCount(1);
        }
    }
}
