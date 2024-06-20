using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace User.Gestion.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Sinistre> Sinistres { get; set; }

        public DbSet<Devis> Devis { get; set; }
        public DbSet<DevisAuto> DevisAuto { get; set; }
        public DbSet<DevisSante> DevisSante { get; set; }
        public DbSet<DevisHabitation> DevisHabitation { get; set; }
        public DbSet<DevisVie> DevisVie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasOne(t => t.Owner)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.OwnerId)
                .IsRequired();

            builder.Entity<Contract>()
          .HasOne(c => c.ApplicationUser)
          .WithMany(u => u.Contracts)
          .HasForeignKey(c => c.UserId);


            builder.Entity<Sinistre>()
                           .HasOne(s => s.ApplicationUser)
                           .WithMany(u => u.Sinistres)
                           .HasForeignKey(s => s.UserId);

            builder.Entity<Devis>()
                .HasOne(d => d.Owner)
                .WithMany(u => u.Devis)
                .HasForeignKey(d => d.OwnerId)
                .IsRequired();

            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                new IdentityRole() { Name = "Client", ConcurrencyStamp = "2", NormalizedName = "Client" }
            );
        }
    }
}
