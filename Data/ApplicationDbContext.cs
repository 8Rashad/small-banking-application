using BankApplication.Entity;
using BankApplication.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BankApplication.DTO;


namespace BankApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CustomerEntity> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.EmailConfirmed)
                    .HasConversion(v => v ? 1 : 0, v => v == 1)
                    .HasColumnType("NUMBER(1)");

                entity.Property(e => e.PhoneNumberConfirmed)
                    .HasConversion(v => v ? 1 : 0, v => v == 1)
                    .HasColumnType("NUMBER(1)");

                entity.Property(e => e.TwoFactorEnabled)
                    .HasConversion(v => v ? 1 : 0, v => v == 1)
                    .HasColumnType("NUMBER(1)");

                entity.Property(e => e.LockoutEnabled)
                    .HasConversion(v => v ? 1 : 0, v => v == 1)
                    .HasColumnType("NUMBER(1)");

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
