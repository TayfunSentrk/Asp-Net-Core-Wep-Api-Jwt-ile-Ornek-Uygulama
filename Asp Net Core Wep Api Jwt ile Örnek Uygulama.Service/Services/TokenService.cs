using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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


        /// <summary>
        /// Token'ın payloadında olması gereken değerler yazıldı
        /// </summary>
        /// 
        /// <returns>List tipinde token'ın payloadında claimsleri döner.</returns>
        private IEnumerable<Claim> GetClaims(UserApp userApp,List<string> audiences)
        {
            var userList = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                 new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                 new Claim(ClaimTypes.Name,userApp.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;

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
