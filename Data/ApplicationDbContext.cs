using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SaucyCapstone.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityUser>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.ProviderKey).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserLogin<string>>().Property(x => x.LoginProvider).HasMaxLength(225);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.Name).HasMaxLength(112);
        modelBuilder.Entity<IdentityUserToken<string>>().Property(x => x.LoginProvider).HasMaxLength(112);
    }
}
