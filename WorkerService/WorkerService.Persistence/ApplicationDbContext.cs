using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Domain.Entities;

namespace WorkerService.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    (x) => x.MigrationsAssembly(_migrationAssembly));
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<Price>()
        .HasOne(p => p.Company)             // Price entity has one Company entity
        .WithMany(c => c.Prices)            // Company entity can have many Price entities
        .HasForeignKey(p => p.CompanyId);   // Foreign key property

            modelBuilder.Entity<Price>()
        .Property(p => p.PriceLTP)
        .HasColumnType("decimal(18,2)"); // Example precision (18) and scale (2)

            modelBuilder.Entity<Price>()
                .Property(p => p.Volume)
                .HasColumnType("decimal(18,2)"); // Example precision (18) and scale (2)

            modelBuilder.Entity<Price>()
                .Property(p => p.Open)
                .HasColumnType("decimal(18,2)"); // Example precision (18) and scale (2)

            modelBuilder.Entity<Price>()
                .Property(p => p.High)
                .HasColumnType("decimal(18,2)"); // Example precision (18) and scale (2)

            modelBuilder.Entity<Price>()
                .Property(p => p.Low)
                .HasColumnType("decimal(18,2)"); // Example precision (18) and scale (2)


            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Company> Company { get; set; }
        public DbSet<Price> Price { get; set; }

    }
}
