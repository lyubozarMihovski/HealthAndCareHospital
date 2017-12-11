namespace HealthAndCareHospital.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using System.Collections.Generic;
    using HealthAndCareHospital.Data.Models;
    using HealthAndCareHospital.Services.Models.Contact;
    using HealthAndCareHospital.Data;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class ContactService : IContactService
    {
        private readonly HealthAndCareHospitalDbContext db;

        public ContactService(HealthAndCareHospitalDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ContactFormServiceModel>> All()
        {
            return await this.db.
                Contacts
                .OrderByDescending(c => c.Id)
                .ProjectTo<ContactFormServiceModel>()
                .ToListAsync();
        }

        public async Task CreateAsync(string name, string email, string subject, string message)
        {
            var contact = new Contact
            {
                Name = name,
                Email = email,
                Subject = subject,
                Message = message
            };

            this.db.Contacts.Add(contact);
            await this.db.SaveChangesAsync();
        }

        public async Task<ContactFormServiceModel> CreateModelAsync(int id)
        {
            var contact = await this.FindByIdAsync(id);
            contact.IsSeen = true;

            await this.db.SaveChangesAsync();

            var model = new ContactFormServiceModel
            {
                Name = contact.Name,
                Email = contact.Email,
                Subject = contact.Subject,
                Message = contact.Message,
                IsSeen = contact.IsSeen
            };

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            var contact = this.db
                .Contacts
                .Where(c => c.Id == id)
                .FirstOrDefault();

            this.db.Contacts.Remove(contact);
            await this.db.SaveChangesAsync();
        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await this.db.Contacts.FindAsync(id);
        }
    }
}
