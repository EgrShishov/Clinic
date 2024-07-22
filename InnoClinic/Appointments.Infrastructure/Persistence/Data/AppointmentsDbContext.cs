using Microsoft.EntityFrameworkCore;

public class AppointmentsDbContext : DbContext
{
    public AppointmentsDbContext(DbContextOptions<AppointmentsDbContext> options) : base(options) 
    {
        Database.EnsureCreated();
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Results> Results { get; set; }
}
