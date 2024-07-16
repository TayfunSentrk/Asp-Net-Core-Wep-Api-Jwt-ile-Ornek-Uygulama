using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.Extensions
{
    public static class CustomExceptionHandler
    {

        public static void UseCustomException(this IApplicationBuilder app)
        {
            /// uygulamadaki tüm hataları yakalamk için middleware
            app.UseExceptionHandler(config =>
            {
                //middleware yazıyorum.Run kullanmanın sebebi bir sonraki middleware geçmez.Burda sonlandıyorum
                config.Run(async context =>
                {
                    //serverden kaynaklı olduğu için
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    //hataları yakalabilmek için
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;
                        ErrorDto errorDto = null;

                        //eper customExceptions tipinde bir hata varsa bunu cliente gösteriyorum
                        //eğer hata varsa reponse dönüyorum
                        if ((ex is CustomExceptions))
                        {
                            errorDto = new ErrorDto(ex.Message, true);
                        }
                        //eper customExceptions tipinde bir hata değilse bunu cliente göstermiyorum
                        else
                        {
                            errorDto = new ErrorDto(ex.Message, false);
                        }

                        var response = Response<NoDataDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));




                    }
                });
            });
        }
    }
}
