namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Concrete.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.Concrete.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            // İlk kurulumda giriş yapabilmek için varsayılan bir admin kullanıcısı.
            // ÖNEMLİ: İlk girişten sonra bu şifreyi mutlaka değiştirin.
            context.Users.AddOrUpdate(
                u => u.UserName,
                new EntityLayer.Concrete.User
                {
                    UserName = "admin",
                    Email = "admin@gategold.local",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    Role = "Admin"
                });

            context.Customers.AddOrUpdate(
    c => c.CariKodu,
    new EntityLayer.Concrete.Customer
    {
        CariKodu = "PERAKENDE",
        CariAdi = "Perakende Müşteri (Genel)",
        CariTipi = "Perakende"
    });

            context.SaveChanges();

            context.SaveChanges();
        }
    }
}
