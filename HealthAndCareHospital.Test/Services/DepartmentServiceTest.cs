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

    public class DepartmentServiceTest
    {
        [Fact]
        public async Task CreateAsyncShouldReturnTrueAndNewDepartment()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            var result = await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();

            result.Should()
                .Be(true);

            db.Departments.Should()
                .HaveCount(1);

        }

        [Fact]
        public async Task EditShouldReturnTrueAndEditedDepartment()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            var result = await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var id = await db.Departments
                .Where(d => d.Name == "Department")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var edited = await departmentService.Edit(id, "EditedDepartment", "Gosho", "SomeURL");
            await db.SaveChangesAsync();

            edited.Should()
                .BeTrue();

            result.Should()
                .Be(true);

            db.Departments.Should()
                .HaveCount(1);

        }
    }
}
