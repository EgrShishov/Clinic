using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AuthDbContext : IdentityDbContext<Account, IdentityRole<int>, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    public DbSet<Account> Accounts { get; set; }
}
