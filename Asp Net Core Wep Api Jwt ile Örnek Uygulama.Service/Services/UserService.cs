using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> userManager;

        public UserService(UserManager<UserApp> userManager)
        {
            this.userManager = userManager;
        }


        /// <summary>
        /// createUserDto parametresine göre eğer başarılı ise UserAppDto döner
        /// </summary>
        /// <param name="createUserDto">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda UserAppDto Döner.CreateUserDto ait veriler ışığında veritabanına kullancı eklenir.</returns>

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp()
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName,
            };

            var result=await userManager.CreateAsync(user,createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);

            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
            
        }


        /// <summary>
        /// userName parametresine göre eğer başarılı ise UserAppDto döner
        /// </summary>
        /// <param name="userName">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda UserAppDto Döner.Verilen isim parametresine göre eğer başarılıysa UserAppDto döner.</returns>

        public async Task<Response<UserAppDto>> GetUserByName(string userName)
        {
            var user=await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("Boyle bir data yok", 404, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }
    }
}
