using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos
{
    public class ClientTokenDto
    { 
        public string AccessToken { get; set; }        //sadece clientler için
          
        public DateTime AccessTokenExpiration { get; set; }
    }
}
