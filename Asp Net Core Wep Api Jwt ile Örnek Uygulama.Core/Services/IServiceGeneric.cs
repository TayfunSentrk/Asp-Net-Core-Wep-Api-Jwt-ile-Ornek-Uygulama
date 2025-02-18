﻿using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Services
{
    public interface IServiceGeneric<TEntity,TDto> where TEntity : class where TDto : class //burda dto ile entity arasında dönüştürme işlemleri 
    {

        Task<Response<TDto>> GetByIdAsync(int id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<Response<TDto>> AddAsync(TDto entity); 

        Task<Response<NoDataDto>> Remove(int id); 

        Task<Response<NoDataDto>> Update(TDto entity,int id);
}
}