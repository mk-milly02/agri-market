using AgriMarket.API.Models.Domain.Auth;
using AgriMarket.API.Models.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgriMarket.API.Data
{
    public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Product> Products { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles into Database
            var userRoleId = "d4162388-82fb-43e8-a918-270d38762156";
            var superAdminRoleId = "a699b2de-7d03-4515-913f-6a722e3f272e";

            var roles = new List<IdentityRole>{
                new(){
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId,
                    Name="User",
                    NormalizedName="USER"
                },
                new(){
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId,
                    Name="SuperUser",
                    NormalizedName="SUPERUSER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }

}