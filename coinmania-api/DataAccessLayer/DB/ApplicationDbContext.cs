using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DB;

public class ApplicationDbContext: IdentityDbContext<User,UserRole,int>
{
    public DbSet<UserInformation> UserInformations { get; set; }
    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=CoinMania;User Id=sa;password=lashagiorgi123;Trusted_Connection=False;MultipleActiveResultSets=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(o => o.GeneralInformation)
            .WithOne(o => o.User)
            .HasForeignKey<UserInformation>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}