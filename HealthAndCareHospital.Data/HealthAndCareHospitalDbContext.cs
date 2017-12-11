namespace HealthAndCareHospital.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;

    public class HealthAndCareHospitalDbContext : IdentityDbContext<User>
    {
        public HealthAndCareHospitalDbContext(DbContextOptions<HealthAndCareHospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Doctor>()
               .HasIndex(d => d.Email)
               .IsUnique();

            builder.Entity<Department>()
               .HasMany(d => d.Doctors)
               .WithOne(dr => dr.Department)
               .HasForeignKey(dr => dr.DepartmentId);

            builder.Entity<Department>()
               .HasMany(d => d.Diseases)
               .WithOne(ds => ds.Department)
               .HasForeignKey(ds => ds.DepartmentId);

            builder.Entity<Doctor>()
               .HasMany(d => d.Receipts)
               .WithOne(r => r.Doctor)
               .HasForeignKey(r => r.DoctorId);

            builder.Entity<Doctor>()
              .HasMany(d => d.Patients)
              .WithOne(p => p.Doctor)
              .HasForeignKey(p => p.DoctorId);

            base.OnModelCreating(builder);
        }
    }
}
