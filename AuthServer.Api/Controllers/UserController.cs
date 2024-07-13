using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {

            return ActionResultInstance(await userService.CreateUserAsync(createUserDto));
        }

        /// <summary>
        /// Identity'den gelen httpcontent ile ilgili isme göre user elde ettik
        /// 
        /// </summary>
      
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olup olmamasını döner..</returns>
        ///

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> GetUser()
        {
           
            return ActionResultInstance(await userService.GetUserByName(HttpContext.User.Identity.Name));
        }


    }
}
