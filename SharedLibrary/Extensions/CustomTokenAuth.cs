using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Configurations;
using SharedLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class CustomTokenAuth
    {
        // program cs'te IserviceCollection istediği için  IServiceCollection ekledim
        public static void AddCustomTokenAuth(this IServiceCollection services,CustomTokenOptions tokenOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// 1tane üyelik sistemi uygulamak için bunu yaptım
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>  //burda claim yerine jsonwebtoken kullanıyorum
            {
               // customtokenoptionsu aldım bunu validation Issuer kısmında kullanıcam
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,  //EĞER BU token dağıtan servis mi değil mi kontrol etmek için
                    ValidAudience = tokenOptions.Audiences[0], // BURDAN BİRDEN FAZLA DİZİ VAR FAKAT birincisi yeterli,
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),//BURDA security key'in setlenmesi
                    ValidateIssuerSigningKey = true,//imza doğrulanması lazım
                    ValidateAudience = true, //burda yetkisi olan yerler doğrulamal
                    ValidateIssuer = true, //tokenı kım sağlıyor onu kontrol etmek
                    ValidateLifetime = true//ömrünü kontrol etmek için
                                           //ClockSkew=TimeSpan.Zero // tüm serverlerde aynı olması için

                };
            });
        }
    }
}
