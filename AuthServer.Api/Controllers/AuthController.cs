using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Api.Controllers
{
    //action bazında işlem yapmak için kullandım
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService service;

        public AuthController(IAuthenticationService service)
        {
            this.service = service;
        }


        /// <summary>
        /// loginDto parametresine göre eğer başarılı ise Başarılı olup olmamasını döner
        /// </summary>
        /// <param name="loginDto">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olup olmamasını döner.loginDto ait veriler ile token oluşturulır.</returns>

        [HttpPost]

        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            //daha dinamik yapmak için basecontroller oluşturduk 
            var result=await service.CreateToken(loginDto);
            return ActionResultInstance(result);
        }


        /// <summary>
        /// clientLoginDto parametresine göre eğer başarılı ise Başarılı olup olmamasını döner
        /// </summary>
        /// <param name="clientLoginDto">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olup olmamasını döner.clientLoginDto ait veriler ile token oluşturulır.</returns>

        [HttpPost]

        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = service.CreateTokenByClient(clientLoginDto);

            return ActionResultInstance(result);
        }

        /// <summary>
        /// refreshToken parametresine göre eğer başarılı ise Başarılı olup olmamasını döner
        /// </summary>
        /// <param name="refreshToken">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olup olmamasını döner.refreshToken ait veriler ile token silme işlemi gerçekleşir.</returns>

        //refresh tokeni silmek için method
        [HttpPost]

        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshToken)
        {
            var result = await service.RevokeRefreshToken(refreshToken.RefreshToken);

            return ActionResultInstance(result);
        }
        /// <summary>
        /// refreshToken parametresine göre eğer başarılı ise Başarılı olup olmamasını döner
        /// </summary>
        /// <param name="refreshToken">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olup olmamasını döner.refreshToken ait veriler ile token oluşturma işlemi gerçekleşir.</returns>

        //refresh tokeni kullarak token oluşturmak
        [HttpPost]

        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshToken)
        {
            var result = await service.CreateTokenByRefreshToken(refreshToken.RefreshToken);

            return ActionResultInstance(result);
        }
    }
}
