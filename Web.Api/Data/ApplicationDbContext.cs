using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.DTO.Data;


namespace Web.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Immigration> Immigrations { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<EmploymentInfo> EmploymentInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasOne<EmploymentInfo>(x => x.EmploymentInfo)
                .WithOne(j => j.Employee)
                .HasForeignKey<EmploymentInfo>(jj => jj.EmployeeId)
                .IsRequired(false);

            modelBuilder.Entity<Address>()
                .HasAlternateKey(x => new { x.EmployeeId, x.AddressType });


            modelBuilder.Entity<EmergencyContact>()
                .HasAlternateKey(x => new { x.EmployeeId, x.RelationshipStatus });

            modelBuilder.Entity<Employee>()
               .HasOne<Immigration>(x => x.Immigration)
               .WithOne(e => e.Employee)
               .HasForeignKey<Immigration>(y => y.EmployeeId)
               .IsRequired(false);

            //modelBuilder.Entity<Employee>()
            //   .HasOne<Address>(x => x.AddressOther)
            //   .WithOne(e => e.Employee)
            //   .HasForeignKey<Address>(y => y.EmployeeId)
            //   .IsRequired(false);

            //modelBuilder.Entity<Employee>()
            //   .HasOne<EmergencyContact>(x => x.EmergencyContact1)
            //   .WithOne(e => e.Employee)
            //   .HasForeignKey<EmergencyContact>(y => y.EmployeeId)
            //   .IsRequired(false);

            //modelBuilder.Entity<Employee>()
            //   .HasOne<EmergencyContact>(x => x.EmergencyContact2)
            //   .WithOne(e => e.Employee)
            //   .HasForeignKey<EmergencyContact>(y => y.EmployeeId)
            //   .IsRequired(false);
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).UpdateDate = DateTime.UtcNow;
            }
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
