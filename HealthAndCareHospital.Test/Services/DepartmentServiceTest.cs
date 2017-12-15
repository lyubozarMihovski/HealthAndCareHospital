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
        public async Task EditShouldReturnTrueAndProperCountOfDepartmentst()
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

        [Fact]
        public async Task DeletShouldReturnTrueAndProperCountOfDepartments()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            var result = await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var id = await db.Departments
                .Where(d => d.Name == "Department")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var deleted = await departmentService.Delete(id);
            await db.SaveChangesAsync();

            deleted.Should()
                .BeTrue();

            result.Should()
                .Be(true);

            db.Departments.Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task FindByNameShouldReturnTrueDepartment()
        {
            var db = Tests.GetDatabase();
            var departmentService = new DepartmentService(db);
            var result = await departmentService.CreateAsync("Department", "Gosho", "SomeURL");
            await db.SaveChangesAsync();
            var finded = await departmentService.FindByName("Department");

            finded.Should().BeOfType<Department>();
        }
    }
}
