namespace HealthAndCareHospital.Test.Services
{
    using FluentAssertions;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class DiseaseServiceTest
    {
        public DiseaseServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllShouldReturnCorrectResultWithCorrectOrder()
        {
            var db = Tests.GetDatabase();
            var department = new Department { Id = 1, Name = "Gosho" };

            var firstDisease = new Disease { Id = 1, Name = "First", Department = department };
            var secondDisease = new Disease { Id = 2, Name = "Second", Department = department };
            var thirdDisease = new Disease { Id = 3, Name = "Third", Department = department };
            var fourthDisease = new Disease { Id = 4, Name = "Fourth", Department = department };

            db.AddRange(firstDisease, secondDisease, thirdDisease, fourthDisease);
            await db.SaveChangesAsync();

            var diseaseService = new DiseaseService(db);

            var result = await diseaseService.All();

            result.Should()
                .HaveCount(4);
        }

        [Fact]
        public async Task SearchShouldReturnCorrectResultWithCorrectOrder()
        {
            var db = Tests.GetDatabase();
            var department = new Department { Id = 1, Name = "Gosho" };

            var firstDisease = new Disease { Id = 1, Name = "First", Department = department };
            var secondDisease = new Disease { Id = 2, Name = "Second", Department = department };
            var thirdDisease = new Disease { Id = 3, Name = "Third", Department = department };
            var fourthDisease = new Disease { Id = 4, Name = "Fourth", Department = department };

            db.AddRange(firstDisease, secondDisease, thirdDisease, fourthDisease);
            await db.SaveChangesAsync();

            var diseaseService = new DiseaseService(db);

            var result = await diseaseService.Search("f");

            result.Should()
                .Match(r => r.ElementAt(0).Id == 1
                && r.ElementAt(1).Id == 4)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task DiseaseExistsShouldReturnCorrectResultTrue()
        {
            var db = Tests.GetDatabase();

            var firstDisease = new Disease { Name = "First" };
            var secondDisease = new Disease { Name = "Second" };
            var thirdDisease = new Disease { Name = "Third" };
            var fourthDisease = new Disease { Name = "Fourth"};

            db.AddRange(firstDisease, secondDisease, thirdDisease, fourthDisease);
            await db.SaveChangesAsync();
            var diseaseService = new DiseaseService(db);

            var result = await diseaseService.DiseaseExists(1);

            result.Should()
                .Be(true);
        }

        [Fact]
        public async Task FindByIdShouldReturnCorrectResultDisease()
        {
            var db = Tests.GetDatabase();

            var firstDisease = new Disease { Id = 1 ,Name = "First"};
            var secondDisease = new Disease { Id = 2, Name = "Second" };
            var thirdDisease = new Disease { Id = 3, Name = "Third" };
            var fourthDisease = new Disease { Id = 4, Name = "Fourth" };

            db.AddRange(firstDisease, secondDisease, thirdDisease, fourthDisease);
            await db.SaveChangesAsync();
            var diseaseService = new DiseaseService(db);

            var result = await diseaseService.FindById(1);

            result.Should()
                .NotBeNull();

            result.Should()
                .BeOfType<Disease>();
        }

        [Fact]
        public async Task CreateShouldReturnTrueAndNewDisease()
        {
            var db = Tests.GetDatabase();
            var diseaseService = new DiseaseService(db);
            var department = new Department { Id = 1, Name = "Gosho" };
            await diseaseService.Create("Name", "Description", department);

            db.Diseases.Should()
                .HaveCount(1);              
        }

        [Fact]
        public async Task EditShouldReturnTrueAndEditedDisease()
        {
            var db = Tests.GetDatabase();
            var diseaseService = new DiseaseService(db);
            var department = new Department { Id = 1, Name = "Gosho" };            
            await diseaseService.Create("Name", "Description", department);
            await db.SaveChangesAsync();
            var id = await db.Diseases
                .Where(d => d.Name == "Name")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var edited = await diseaseService
                .Edit(id, "EditedName", "Description", department.Name);

            edited.Should()
                .BeTrue();

            db.Diseases.Should()
                .HaveCount(1);
        }
    }
}
