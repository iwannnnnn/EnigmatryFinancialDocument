using EnigmatryFinancialDocument.Core.Entities;
using EnigmatryFinancialDocument.Core.Entities.FinDoc.ProductA;
using EnigmatryFinancialDocument.Core.Entities.FinDoc.ProductB;
using Microsoft.EntityFrameworkCore;

namespace EnigmatryFinancialDocument.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<FinancialDocument> FinancialDocuments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Client> Clients { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(p => p.ProductCode);


            modelBuilder.Entity<FinancialDocument>().HasKey(f => f.DocumentId);

            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            
            modelBuilder.Entity<Client>().HasKey(t => t.ClientId);

            modelBuilder.Entity<Tenant>().HasKey(t => t.TenantId);

            //modelBuilder.Entity<FinancialDocument>()
            //    .HasMany(fd => fd.Transactions)
            //    .WithOne();
        }
    }
}
