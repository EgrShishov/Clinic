using InnoProfileslinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Profiles.Infrastructure.Persistence.Data
{
    public class ProfilesDbContext : DbContext
    {
        public ProfilesDbContext(DbContextOptions<ProfilesDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
    }
}
