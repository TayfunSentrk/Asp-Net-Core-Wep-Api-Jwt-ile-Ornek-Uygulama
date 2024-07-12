using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data.Configration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Configration yapabilmek için  IEntityTypeConfiguration implemente ediyorum.
        /// </summary>
       
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);//burda id primary key olduğunu  kontrol ediyorum

            builder.Property(p=> p.Name).IsRequired().HasMaxLength(200); //burda isim alanı zorunlu max  length belirtildi

            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");// toplamda 18 karakter virgülden sonra 2 karakter tutulması sağlandı

            builder.Property(p=>p.UserId).IsRequired();//kullanıcısı olmak zorunda
        }
    }
}
