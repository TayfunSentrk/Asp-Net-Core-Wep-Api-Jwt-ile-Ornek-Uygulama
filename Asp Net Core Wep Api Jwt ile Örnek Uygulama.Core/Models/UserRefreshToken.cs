﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; } //hangi kullanıcıya ait
        public string Code { get; set; } // refresh token 
        public DateTime Expiration { get; set; } //refresh token ömrü
    }
}
