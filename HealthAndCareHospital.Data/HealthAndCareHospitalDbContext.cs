namespace HealthAndCareHospital.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HealthAndCareHospitalDbContext : IdentityDbContext<User>
    {
        public HealthAndCareHospitalDbContext(DbContextOptions<HealthAndCareHospitalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
