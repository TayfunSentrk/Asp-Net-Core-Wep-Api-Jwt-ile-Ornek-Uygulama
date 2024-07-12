using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Configurations
{
    public class CustomTokenOptions
    {

        /// <summary>
        /// Appsettin json'a tanımladığımız yerlerle aynı yapıları kullandım
        /// Burda Auidence Kısmında hangi apile're istek yapılır 
        /// Issuer token dağıtan adres
        /// Access Token'ın Süresi
        /// Refresh Token'ın Süresi
        /// Security Key
        /// </summary>

        public List<string> Audiences { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
