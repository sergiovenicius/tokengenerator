using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tokengenerator.Database
{
    public class DBCardContext : DbContext
    {
        public DBCardContext(DbContextOptions<DBCardContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DBCard> Card { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBCard>()
                .HasKey(p => new { p.CardId });

            modelBuilder.Entity<DBCard>().Property(b => b.CardId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DBCard>()
                .Property(b => b.RegistrationDate)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<DBCard>().Property(b => b.CustomerId)
                .IsRequired();
            modelBuilder.Entity<DBCard>().Property(b => b.CardNumber)
                .IsRequired();
            modelBuilder.Entity<DBCard>().Property(b => b.CVV)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }
    }
}
