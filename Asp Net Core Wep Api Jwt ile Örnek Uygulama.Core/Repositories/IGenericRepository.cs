using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // İd göre entity getirme

        Task<IEnumerable<T>> GetAllAsync(); // tüm entityleri getirme

        IQueryable<T> Where(Expression<Func<T,bool>> predicate);

        Task AddAsync(T entity); //Ürün Ekleme

        void Remove(T entity); //Ürün Silme 

        T Update(T entity);//Ürün Güncelleme

    }
}
