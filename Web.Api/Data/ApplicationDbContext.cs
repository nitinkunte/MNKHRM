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

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<ImmigrationModel> Immigrations { get; set; }
        public DbSet<EmergencyContactModel> EmergencyContacts { get; set; }
        public DbSet<EmploymentInfoModel> EmploymentInfos { get; set; }
        public DbSet<ProfitabilityModel> Profitabilities { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        public DbSet<SessionModel> SessionModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmployeeModel>()
                .HasOne<EmploymentInfoModel>(x => x.EmploymentInfo)
                .WithOne(j => j.Employee)
                .HasForeignKey<EmploymentInfoModel>(jj => jj.EmployeeId)
                .IsRequired(false);

            modelBuilder.Entity<AddressModel>()
                .HasAlternateKey(x => new { x.EmployeeId, x.AddressType });

            modelBuilder.Entity<EmergencyContactModel>()
                .HasAlternateKey(x => new { x.EmployeeId, x.RelationshipStatus });

            modelBuilder.Entity<EmployeeModel>()
               .HasOne<ImmigrationModel>(x => x.Immigration)
               .WithOne(e => e.Employee)
               .HasForeignKey<ImmigrationModel>(y => y.EmployeeId)
               .IsRequired(false);

            modelBuilder.Entity<EmployeeModel>()
               .HasOne<ProfitabilityModel>(x => x.Profitability)
               .WithOne(e => e.Employee)
               .HasForeignKey<ProfitabilityModel>(y => y.EmployeeId)
               .IsRequired(false);

            modelBuilder.Entity<UserRoleModel>()
                .HasAlternateKey(x => x.UserName);

            modelBuilder.Entity<UserRoleModel>()
                .HasIndex(x => x.UserName);

            modelBuilder.Entity<SessionModel>()
                .HasIndex(x => x.UserName);
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
