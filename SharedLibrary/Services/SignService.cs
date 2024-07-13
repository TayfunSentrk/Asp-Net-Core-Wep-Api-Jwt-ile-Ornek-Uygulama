using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public static class SignService
    {

        /// <summary>

        ///Simetrik key imza ve doğrulama aynı string tipine ait
        ///Public key ile private key aynı
        /// </summary>

        /// <returns>Bir security key vererek SymmetricSecurityKey dönüyoruz </returns>

        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
