using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; } //veritabanında email ile password eşleşirse token dönücem
        public string Password { get; set; }
    }
}
