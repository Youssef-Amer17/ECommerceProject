using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceProject.Data
{
    // EF Core will call this automatically when adding migrations
    internal class ECommerceDbContextFactory : IDesignTimeDbContextFactory<ECommerceDbContext>
    {
        public ECommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new ECommerceDbContext(optionsBuilder.Options);
        }
    }
}
