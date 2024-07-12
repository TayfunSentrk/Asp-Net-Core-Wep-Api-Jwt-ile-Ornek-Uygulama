using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Configration;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
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

        private readonly IGenericRepository<UserRefreshToken> userRefreshToken;
        /// <summary>
        ///  Burda clientler için client listesi ve bu clientler options pattern uyguladığım için direkt appsetttin json'dan alıcanak
        ///  Kullanıcı doğrulama için usermanager çağrıldı
        ///  database kayıt işlemler için unitof work tanımlandı
        ///  Refresh token database kaydetmek için generic repository çağırldı
        ///  Token serviste yer akan methodları kullanabilmek için Itoken service çağırıldı
        /// </summary>
     
       
        public AuthenticationService(IOptions<List<Client>> client, ITokenService tokenService, UserManager<UserApp> userManager, IUnitofWork unitofWork, IGenericRepository<UserRefreshToken> userRefreshToken)
        {
            _client = client.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            this.unitofWork = unitofWork;
            this.userRefreshToken = userRefreshToken;
        }

        public Task<Response<TokenDto>> CreateToken(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
