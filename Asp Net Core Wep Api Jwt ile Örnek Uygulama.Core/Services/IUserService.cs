using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services
{
    public interface IUserService
    {
        /// <summary>
        /// UserApp Dto dönülür
        /// </summary>
        /// <param name="createUserDto">Kayıt olurken gerekli  bilgileri içerir.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda UserAppDto döner.</returns>
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        /// <summary>
        /// UserApp Dto dönülür
        /// </summary>
        /// <param name="userName">Kayıt bilgileri getirirken gerekli user name.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda UserAppDto döner.</returns>
        Task<Response<UserAppDto>> GetUserByName(string userName);
    }
}
