using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// Üyelik sistemi gerektirmeyen clientlar için Claim Listesi Oluşturuldu
        /// </summary>
        /// 
        /// <returns>List tipinde token'ın payloadında claimsleri döner.</returns>

        public IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
           claims.Add(new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString()));    //öznesi kimin için onun için 

            return claims;

        }

        /// <summary>
        /// İlk olarak bu token  hangi user'a ait  olucak onu parametre'de belirttim
        /// Burda Access Token ve Refresh Token süreleri customtoken'dan geliyor
        /// security key simetrik olarak oluşturuk ve ilgili security key custom token'dan aldık
        /// SigningCredentials ile imzalama algoritması oluşturuldu
        /// jwtSecurityToken token için gerekli özellikler belirlendi
        /// JwtSecurityTokenHandler.writetoken methodu ile token oluşturuldu
        /// Oluşturulan token tokendto'ya çevrildi
        /// </summary>
        /// 
        /// <returns>Token Dto döner.</returns>

        public TokenDto CreateToken(UserApp user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(customToken.AccessTokenExpiration); //tokenun ömr
            var refreshTokenExpiration = DateTime.Now.AddMinutes(customToken.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(customToken.SecurityKey);//tokenu imzalicak key

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);//Algoritma ile imza yapıyı oluşturduk
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: customToken.Issuer, expires: accessTokenExpiration,
                notBefore: DateTime.Now, claims: GetClaims(user, customToken.Audiences), signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            
            var token=handler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto()
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }


        /// <summary>
        /// İlk olarak bu token  hangi client'a ait  olucak onu parametre'de belirttim
        /// Burda Access Token customtoken'dan geliyor
        /// security key simetrik olarak oluşturuk ve ilgili security key custom token'dan aldık
        /// SigningCredentials ile imzalama algoritması oluşturuldu
        /// jwtSecurityToken token için gerekli özellikler belirlendi
        /// JwtSecurityTokenHandler.writetoken methodu ile token oluşturuldu
        /// Oluşturulan token ClientTokenDto'ya çevrildi
        /// </summary>
        /// 
        /// <returns>ClientTokenDto döner.</returns>


        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(customToken.AccessTokenExpiration); //tokenun ömr
         

            var securityKey = SignService.GetSymmetricSecurityKey(customToken.SecurityKey);//tokenu imzalicak key

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);//Algoritma ile imza yapıyı oluşturduk
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: customToken.Issuer, expires: accessTokenExpiration,
                notBefore: DateTime.Now, claims:GetClaimsByClient(client), signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);
            var clientTokenDto = new ClientTokenDto()
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
               
            };

            return clientTokenDto;

        }
    }
}
