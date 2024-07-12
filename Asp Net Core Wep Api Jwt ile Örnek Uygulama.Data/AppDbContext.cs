using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data
{
    public class AppDbContext:IdentityDbContext<UserApp,IdentityRole,string>
    {


        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Refresh tokenin süresinin ve değerinin ve hangi user'a ait olduğunu belirten tabla
        /// </summary>
        /// <param name="options"></param>
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


        /// <summary>
        /// Configration tarafında appdbcontext base gönderiyoruz
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);//burda ıEntityType Configrationları bulup uygular
            base.OnModelCreating(builder);
        }
    }
}
