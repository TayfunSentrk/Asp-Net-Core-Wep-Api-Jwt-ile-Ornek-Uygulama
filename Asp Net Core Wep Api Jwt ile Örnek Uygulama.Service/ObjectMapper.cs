using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service
{
    public static  class ObjectMapper
    {
        /// <summary>
        /// Burda lazy yaptım çünkü uygulamada ne zaman çağırılırsa ozaman memory alınsın
        /// </summary>
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });


        public static IMapper Mapper => lazy.Value;  //burda set yapmıyorum sadece get' alıyorum ben object.mapper çağırdığımız zaman bu memory'e kayıt olucak
    }
}
