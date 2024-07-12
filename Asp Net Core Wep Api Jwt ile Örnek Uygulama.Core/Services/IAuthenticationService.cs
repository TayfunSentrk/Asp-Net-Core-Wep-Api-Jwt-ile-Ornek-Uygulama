using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services
{
    public interface IAuthenticationService
    {
        //bu katman api ile haberleşcek ve dll tarafında örneklemesini yapıcaz

        Task<Response<TokenDto>> CreateToken(LoginDto loginDto); //login dto doğruysa burda tokendto dönüyorum
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);//burda üye işlemleri daha önceden oluşturulmuş ama access token ömrü bitmiş

        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken); // burda refresh tokenı sonlandırmak için refresh token alınır null'e set etmek için

        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto); //Clientler için üyelik olmadan client id ve secrete göre clienttokendto dönülür





                                                                 
    }
}//
