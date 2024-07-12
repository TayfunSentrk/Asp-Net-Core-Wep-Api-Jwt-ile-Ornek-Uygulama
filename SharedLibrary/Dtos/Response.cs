using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.Dtos
{
    public class Response<T> where T : class  //referans tipli olması sağlandı
    {
        public T Data { get; private set; }

        public int StatusCode { get; private set; } //başarılı başarısız olma durumuna göre 
        public ErrorDto Error { get; private set; } //başarılı başarısız olma durumuna göre 
        [JsonIgnore] //serialeze edilmesini engelemek için
        public bool IsSuccesful { get; private set; } // cliente açmmıyorum.Api'lerde kullanaılmak üzere başarılı olup olmadığını kontrol etmek için

        public static Response<T> Success(T data,int statusCode) //başarılı olması durumunda data ve status code'in setlenmesi
        {
            return new Response<T>()
            {
                StatusCode = statusCode,
                Data = data,
                IsSuccesful=true
            };
        }

        public static Response<T> Sucess(int statusCode)
        {
            return new Response<T>() { Data = default, StatusCode = statusCode,IsSuccesful=true };// başarılı olması durumunda datayı dönmüyorum.Default değer verip boş bir değer verildi
        }

        public Response<T> Fail(ErrorDto dto, int statusCode)  //birden fazla hata olması durumunda 
        {
            return new Response<T>() { StatusCode = statusCode, Error = dto,IsSuccesful=false };

        }

        public static Response<T> Fail(string errorMessage,int statusCode,bool isShow) //isShow clientlere gösterilip gösterilmemesi
        {
            var errorDto = new ErrorDto(errorMessage,isShow);

            return new Response<T> { Error = errorDto, StatusCode = statusCode,IsSuccesful=false};
        }
    }
}
