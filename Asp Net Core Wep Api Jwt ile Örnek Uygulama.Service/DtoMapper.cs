using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service
{
    public class DtoMapper:Profile
    {
        /// <summary>
        /// Burda product ve useraPP için mapping işlemleri yaptık.Burda tersine de olabilceği için reverve map yaptım.
        /// </summary>
        public DtoMapper()
        {
           
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<UserAppDto,UserApp>().ReverseMap();   
        }
    }
}
