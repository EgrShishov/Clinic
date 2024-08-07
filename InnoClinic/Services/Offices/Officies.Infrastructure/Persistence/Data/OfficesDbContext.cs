using Microsoft.EntityFrameworkCore;

public class OfficesDbContext : DbContext
{
    public OfficesDbContext(DbContextOptions<OfficesDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Office> Offices { get; set; }
}