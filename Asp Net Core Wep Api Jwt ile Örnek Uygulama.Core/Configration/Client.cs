using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration
{
    public class Client       
                                            //bunlar mobil uygulaması olabilir herhangi entity yada dto değil 
    {
        public string Id { get; set; }  


        public string Secred { get; set; } //password gibi

        //wwww.myapi1.com gibi bunlara erişim sağlayabilir
        public List<string> Audiences { get; set; } // hangi apilere erişim sağlayacak onu belirleyiyoruz
    }
}
