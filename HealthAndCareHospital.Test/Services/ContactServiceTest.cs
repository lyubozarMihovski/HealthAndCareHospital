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

    public class ContactServiceTest
    {
        [Fact]
        public async Task CreateAsyncShouldReturnTrueAndNewContact()
        {
            var db = Tests.GetDatabase();
            var contactService = new ContactService(db);
            var result = await contactService.CreateAsync("Chicho",
                "chicho@gosho.bg",
                "Bolen sum" ,
                "Boli meeeeeeeee");

            result.Should()
                .BeTrue();

            db.Contacts.Should()
                .HaveCount(1);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnTrueAndDeletedContact()
        {
            var db = Tests.GetDatabase();
            var contactService = new ContactService(db);
            var result = await contactService.CreateAsync("Chicho",
                "chicho@gosho.bg",
                "Bolen sum",
                "Boli meeeeeeeee");
            await db.SaveChangesAsync();
            var id = await db.Contacts
                .Where(d => d.Name == "Chicho")
                .Select(d => d.Id)
                .FirstOrDefaultAsync();
            var deleted = await contactService.DeleteAsync(id);
            await db.SaveChangesAsync();

            result.Should()
                .BeTrue();

            deleted.Should()
                .BeTrue();

            db.Contacts.Should()
                .HaveCount(0);
        }
    }
}
