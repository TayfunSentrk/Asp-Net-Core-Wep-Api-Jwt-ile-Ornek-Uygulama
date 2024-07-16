using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class CustomValidationResponse
    {

        //burda modelstate gelen hatalar için override ettik ilk olarak modelstate gelen değerleri error message listesine çevirdik
        //daha sonra bunu elimizde var olan Error dto ilave ettik
        //Iaction Result döndüğü için biz badrequestobjectresult çevirdik çünkü burda yanı zamanda data göndermek istiyoruz
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count() > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto error = new ErrorDto(errors.ToList(), true);

                    var response = Response<NoContentResult>.Fail(error, 400);

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
