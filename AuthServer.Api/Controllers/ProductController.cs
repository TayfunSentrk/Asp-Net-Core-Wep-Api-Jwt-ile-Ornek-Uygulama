using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Dtos;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Models;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Api.Controllers
{
    //tokena sahip ise bu işlemleri yapar
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController :BaseController
    {


        private readonly IServiceGeneric<Product, ProductDto> _productService;

        public ProductController(IServiceGeneric<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Başarılı olması durumunda Productları döner
        /// </summary>
        /
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olması durumunda product listesini döner.</returns>

        [HttpGet]

        public async Task<IActionResult> GetProduct()
        {
            return ActionResultInstance(await _productService.GetAllAsync());
        }


        /// <summary>
        /// ProductDto parametresine göre Yeni bir product veritabanına ekler
        /// </summary>
        /// <param name="ProductDto">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olursa veritabanı product ekler.</returns>
        [HttpPost]

        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productService.AddAsync(productDto));
        }

        /// <summary>
        /// productDto parametresine göre  Product nesnesini update işlemi yapılır
        /// </summary>
        /// <param name="productDto">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olursa veritabanı product nesnesini günceller.</returns>

        [HttpPut]

        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productService.Update(productDto, productDto.Id));
        }


        /// <summary>
        /// ) parametresine göre  Product nesnesini veritabanından siler
        /// </summary>
        /// <param name=")">Token oluşturmak nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda başarılı olursa veritabanı product nesnesini siler.</returns>

        [HttpDelete]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionResultInstance(await _productService.Remove(id));
        }
    }
}
