using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services;
using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Service.Services
{
    public class GenericService<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto: class
    {
        private readonly IUnitofWork unitofWork;

        private readonly IGenericRepository<TEntity> genericRepository;

        public GenericService(IUnitofWork unitofWork, IGenericRepository<TEntity> genericRepository)
        {
            this.unitofWork = unitofWork;
            this.genericRepository = genericRepository;
        }
        /// <summary>
        /// Yeni bir nesne yükler
        /// </summary>
        /// <param name="TDto">Eklenmek istenen nesne ilgili bilgileri içeren veri transfer nesnesi.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda eklenen nesnenerin dtosunu içeren verileri döner.</returns>
        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity=ObjectMapper.Mapper.Map<TEntity>(entity);

            await genericRepository.AddAsync(newEntity);
            await unitofWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200);
        }

        /// <summary>
        /// List şeklinde Tdo nesnelerini döner
        /// </summary>

        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda List şeklinde Tdo nesnelerini döner.</returns>
        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
          
           return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<List<TDto>>(await genericRepository.GetAllAsync()),200);
        }


        /// <summary>
        /// Parametre olaran verilen id'ye göre TDto döner
        /// </summary>
        /// <param name="id">Parametre olarak verilen id .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda parametre olarak verilen id'ye göre TDto döner.</returns>
        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
         var data=await genericRepository.GetByIdAsync(id);

            if(data!=null)
            {
                return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(await genericRepository.GetByIdAsync(id)),200);
            }

            return Response<TDto>.Fail("Böyle bir data bulunamadı", 404, isShow: true);
        }


        /// <summary>
        /// Parametre olaran verilen id'ye veri silme işlemi gerçekleşir
        /// </summary>
        /// <param name="id">Parametre olarak verilen id .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda parametre olarak verilen id'ye göre ilgili nesneye silerek, Herhangi bir veri kümesi olmayan NoDataDto döner.</returns>
        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var data = await genericRepository.GetByIdAsync(id);

            if (data != null)
            {
                genericRepository.Remove(data);
                await unitofWork.CommitAsync();
                return Response<NoDataDto>.Success(204);
            }

         
            return Response<NoDataDto>.Fail("böyle bir data veri tabanında yok",404, isShow: true);
        }




        /// <summary>
        /// Parametre olaran verilen id'ye veri güncelleme işlemi gerçekleşir
        /// </summary>
        /// <param name="id">Parametre olarak verilen id .</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda parametre olarak verilen id'ye göre ilgili nesneyi günceller, Herhangi bir veri kümesi olmayan NoDataDto döner.</returns>
      
        public async Task<Response<NoDataDto>> Update(TDto entity,int id)
        {
           var data=await genericRepository.GetByIdAsync(id);

            if (data != null)
            {
                var updatedData = ObjectMapper.Mapper.Map(entity, data);

                genericRepository.Update(updatedData);

                await unitofWork.CommitAsync();

                return Response<NoDataDto>.Success(204);
            }

            return Response<NoDataDto>.Fail("Böyle bir data veritabanında yok",404,isShow: true);
        }


        /// <summary>
                ///IQuerable kullanılmasının sebebi ddata veri tabanına gitmeden ilgili linq sorgularının yapılmasına olanak sağlar
                ///Tolistasync dediğimiz zaman veritabanından yansır.
        /// </summary>

        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucunda IEnumerable<TDto> tipinde nesneleri döner </returns>



        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var dataList = genericRepository.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await dataList.ToListAsync()),200);
        }
    }
}
