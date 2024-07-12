using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos
{
    public class ClientLoginDto
    {
        public string ClientId { get; set; }  //burda bir üyelik sistemi olmadan client ıd ve clientsecret ile giriş yapıcal
        public string ClientSecret { get; set; }
    }
    }

