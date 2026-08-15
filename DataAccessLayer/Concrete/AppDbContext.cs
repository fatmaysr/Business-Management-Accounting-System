using System;
using System.Collections.Generic;
using System.Data.Entity;
using EntityLayer.Concrete;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DataAccessLayer.Concrete;

namespace DataAccessLayer.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext") { }


        // DbSet'ler (tablolarla eşleşen class'lar)
        //public DbSet<Admin> Admins { get; set; }
        //public DbSet<AdminLog> AdminLogs { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<AdminLog> AdminLogs { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<CaseTransaction> CaseTransactions { get; set; }
        public DbSet<CashCase> CashCases { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FastSale> FastSales { get; set; }
        public DbSet<FastSaleCategory> FastSaleCategories { get; set; }
        public DbSet<FastSaleProduct> FastSaleProducts { get; set; }

        public DbSet<User> Users { get; set; }  

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Sale> Sales { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
                .HasRequired(b => b.Customer)
                .WithMany(c => c.Banks)
                .HasForeignKey(b => b.CariID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankAccount>()
                .HasRequired(ba => ba.Bank)
                .WithMany(b => b.BankAccounts)
                .HasForeignKey(ba => ba.BankId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BankAccount>()
                .HasRequired(ba => ba.Customer)
                .WithMany()
                .HasForeignKey(ba => ba.CariID)
                .WillCascadeOnDelete(false);

            // CashCase ile Customer ilişkisi
            modelBuilder.Entity<CashCase>()
                .HasRequired(c => c.Customer)
                .WithMany()
                .HasForeignKey(c => c.CariID)
                .WillCascadeOnDelete(false);

            // CaseTransaction ile Customer ilişkisi
            modelBuilder.Entity<CaseTransaction>()
                .HasRequired(ct => ct.Customer)
                .WithMany()
                .HasForeignKey(ct => ct.CariID)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);


           

            modelBuilder.Entity<Sale>()
    .       HasRequired(s => s.CashCase)
    .       WithMany()
            .HasForeignKey(s => s.CaseId)
            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CariID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Product)
                .WithMany()
                .HasForeignKey(s => s.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}

        //public DbSet<CustomerLog> CustomerLogs { get; set; }
        //public DbSet<Dats> Dats { get; set; }
        //public DbSet<ExchangeData> ExchangeDatas { get; set; }
        //public DbSet<ExchangeRate> ExchangeRates { get; set; }
        //public DbSet<ExchangeRateNew> ExchangeRateNews { get; set; }
        //public DbSet<ExchangeSetting> ExchangeSettings { get; set; }
       
        //public DbSet<Test> Tests { get; set; }
        //public DbSet<EntityLayer.Concrete.Type> Types { get; set; }
    



