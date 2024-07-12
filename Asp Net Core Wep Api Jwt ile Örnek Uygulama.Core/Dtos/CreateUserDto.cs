using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos
{
    public class CreateUserDto
    {
        /// <summary>
        /// kullancı kaydı için gerekli bilgiler
        /// </summary>
        public string UserName { get; set; } //kullancı kayıt esnasında gerekli bilgiler oluşturuldu
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
