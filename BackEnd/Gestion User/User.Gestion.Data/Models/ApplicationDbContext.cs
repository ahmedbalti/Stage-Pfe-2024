

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

        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<Contract>? Contracts { get; set; }
        public DbSet<Sinistre>? Sinistres { get; set; }
        public DbSet<Devis>? Devis { get; set; }
        public DbSet<Opportunity>? Opportunities { get; set; }
        public DbSet<DevisAuto>? DevisAuto { get; set; }
        public DbSet<DevisSante>? DevisSante { get; set; }
        public DbSet<DevisHabitation>? DevisHabitation { get; set; }
        public DbSet<DevisVie>? DevisVie { get; set; }
        public DbSet<TicketResponse>? TicketResponses { get; set; }
        public DbSet<Feedback>? Feedbacks { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuration des relations et des types de colonnes

            builder.Entity<Ticket>()
                .HasOne(t => t.Owner)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.OwnerId)
                .IsRequired();

            builder.Entity<Contract>()
                .HasOne(c => c.ApplicationUser)
                .WithMany(u => u.Contracts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete


            builder.Entity<Contract>()
                .HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete



            builder.Entity<Sinistre>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sinistres)
                .HasForeignKey(s => s.UserId);

            builder.Entity<Devis>()
                .HasOne(d => d.Owner)
                .WithMany(u => u.Devis)
                .HasForeignKey(d => d.OwnerId)
                .IsRequired();

            builder.Entity<Opportunity>()
                .HasOne(o => o.Devis)
                .WithMany(d => d.Opportunities)
                .HasForeignKey(o => o.DevisId)
                .OnDelete(DeleteBehavior.NoAction); // Correction pour éviter les cycles de clé étrangère

            builder.Entity<Opportunity>()
                .HasOne(o => o.User)
                .WithMany(u => u.Opportunities)
                .HasForeignKey(o => o.UserId);

            builder.Entity<TicketResponse>()
              .HasOne(tr => tr.Ticket)
              .WithMany(t => t.Responses)
              .HasForeignKey(tr => tr.TicketId)
              .IsRequired();

            // Spécification des types de colonnes pour les décimales
            builder.Entity<Sinistre>()
                .Property(s => s.MontantEstime)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Sinistre>()
                .Property(s => s.MontantPaye)
                .HasColumnType("decimal(18,2)");

            builder.Entity<DevisVie>()
                .Property(d => d.Capital)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Opportunity>()
                .Property(o => o.Montant)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Opportunity>()
                .Property(o => o.PrimeAnnuelle)
                .HasColumnType("decimal(18,2)");


            builder.Entity<Feedback>()
               .HasOne(f => f.User)
               .WithMany(u => u.Feedbacks)
               .HasForeignKey(f => f.UserId)
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
