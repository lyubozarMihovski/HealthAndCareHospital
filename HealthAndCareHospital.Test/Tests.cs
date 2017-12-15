namespace HealthAndCareHospital.Test
{
    using AutoMapper;
    using HealthAndCareHospital.Common.Infrastructure.Mapping;
    using HealthAndCareHospital.Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                testsInitialized = true;
            }
        }

        public static HealthAndCareHospitalDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<HealthAndCareHospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new HealthAndCareHospitalDbContext(dbOptions);
        }
    }
}
