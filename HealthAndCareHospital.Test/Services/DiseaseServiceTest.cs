namespace HealthAndCareHospital.Test.Services
{
    using AutoMapper;
    using FluentAssertions;
    using HealthAndCareHospital.Common.Infrastructure.Mapping;
    using HealthAndCareHospital.Data;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Implementations;
    using HealthAndCareHospital.Services.Models.Admin;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class DiseaseServiceTest
    {
        public DiseaseServiceTest()
        {
            Tests.Initialize();
        }

        private HealthAndCareHospitalDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<HealthAndCareHospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new HealthAndCareHospitalDbContext(dbOptions);
        }

        [Fact]
        public async Task AllShouldReturnCorrectResultWithCorrectOrder()
        {
            var db = this.GetDatabase();
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
            var db = this.GetDatabase();
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
            var db = this.GetDatabase();

            var firstDisease = new Disease { Name = "First" };
            var secondDisease = new Disease { Name = "Second" };
            var thirdDisease = new Disease { Name = "Third" };
            var fourthDisease = new Disease { Name = "Fourth"};

            db.AddRange(firstDisease, secondDisease, thirdDisease, fourthDisease);
            await db.SaveChangesAsync();
            var diseaseService = new DiseaseService(db);

            var result = await diseaseService.DiseaseExists(1);

            result.Should().Be(true);
        }

        [Fact]
        public async Task FindByIdShouldReturnCorrectResultDisease()
        {
            var db = this.GetDatabase();

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
    }
}
