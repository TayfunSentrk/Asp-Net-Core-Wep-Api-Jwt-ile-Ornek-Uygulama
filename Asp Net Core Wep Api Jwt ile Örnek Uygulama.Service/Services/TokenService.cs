using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service.Services
{
    public class TokenService : ITokenService
    {

        private readonly UserManager<UserApp > _userManager;

        private readonly CustomTokenOptions customToken;
        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOptions> options)
        {
            _userManager = userManager;
            this.customToken = options.Value;
        }

        //32 bytelık random değer üretir
        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);
            
            return Convert.ToBase64String(numberByte);
        }
        public TokenDto CreateToken(UserApp user)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
