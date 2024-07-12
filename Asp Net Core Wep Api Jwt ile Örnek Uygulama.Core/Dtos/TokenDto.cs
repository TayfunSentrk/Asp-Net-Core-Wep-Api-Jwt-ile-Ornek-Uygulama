using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; } //bu dataları veri tabanına kaydeciem
        public DateTime RefreshTokenExpiration { get; set; } //bu dataları veri tabanına kaydeciem
    }
}
