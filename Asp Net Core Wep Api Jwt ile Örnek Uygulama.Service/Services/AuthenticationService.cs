using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {


        private readonly List<Client> _client;

        private readonly ITokenService _tokenService;

        private readonly UserManager<UserApp> _userManager;

        private readonly IUnitofWork unitofWork;

        private readonly IGenericRepository<UserRefreshToken> userRefreshTokenService;
        /// <summary>
        ///  Burda clientler için client listesi ve bu clientler options pattern uyguladığım için direkt appsetttin json'dan alıcanak
        ///  Kullanıcı doğrulama için usermanager çağrıldı
        ///  database kayıt işlemler için unitof work tanımlandı
        ///  Refresh token database kaydetmek için generic repository çağırldı
        ///  Token serviste yer akan methodları kullanabilmek için Itoken service çağırıldı
        /// </summary>
     
       
        public AuthenticationService(IOptions<List<Client>> client, ITokenService tokenService, UserManager<UserApp> userManager, IUnitofWork unitofWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _client = client.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            this.unitofWork = unitofWork;
            this.userRefreshTokenService = userRefreshTokenService;
        }

        /// <summary>
        /// LoginDto parametresine göre eğer başarılı ise tokendto döner ve veritabanına refresh token kaydeder
        /// </summary>
        /// <param name="loginDto">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda Token Dto Döner.Eğer kullanıcıya ait refresh token yoksa yeni refresh token oluşturur.Eğer var ise refresh tokenin ismini ve süresini değiştirir.</returns>
        public async Task<Response<TokenDto>> CreateToken(LoginDto loginDto)
        {
            if (loginDto == null)  throw new ArgumentNullException(nameof(loginDto));

            var user=await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Response<TokenDto>.Fail("Email ya da password yanlış", 400, isShow: true); //client hata olduğu için 400 hatası verdim.
            }

            if(! await _userManager.CheckPasswordAsync(user,loginDto.Password))
             {
                return Response<TokenDto>.Fail("Email ya da password yanlış", 400, isShow: true); //client hata olduğu için 400 hatası verdim.
            }

            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync(); //refresh token var mı yok mu onu kontrol ediyorum

            if(userRefreshToken == null)
            {
                    await userRefreshTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }

            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await unitofWork.CommitAsync();

            return Response<TokenDto>.Success(token, 200);
        }



        /// <summary>
        /// clientLoginDto parametresine göre eğer başarılı ise ClientTokenDto döner
        /// </summary>
        /// <param name="clientLoginDto">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda ClientTokenDto Döner.Eğer client doğru şekilde giriş yaparsa access tokeni oluşturulur.</returns>

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _client.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secred == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("client ıd veya client secret bulunamadı",404,isShow: true);
            }

            var token=_tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token,200);
        }


        /// <summary>
        /// refreshToken parametresine göre eğer başarılı ise TokenDto döner
        /// </summary>
        /// <param name="refreshToken">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda TokenDto Döner.Eğer parametre verilen refreh token doğru ve token'a ait kullanıcı var ise refresh token revize edilir.Veritabanında güncellenir.</returns>
        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh Token bulunamadı",404, isShow: true);   
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if(user == null)
            {
                return Response<TokenDto>.Fail("User Id bulunamadı", 404, isShow: true);
            }

            var tokenDto=_tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            await unitofWork.CommitAsync();
            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
