using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        /// Tablolarda işlem yapabilmek için dbset oluşturuldu
        /// </summary>

        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
            this._dbSet=appDbContext.Set<T>();
        }

       
        public async Task AddAsync(T entity)
        {
          await _dbSet.AddAsync(entity); //memmory' sadece eklencek eğer unit of work savechange çağırdığmız anda veritabanına eklencek
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _appDbContext.Entry(entity).State = EntityState.Detached;// burda entity null gelmiyorsa memori'de track edilmesini uyguluyorum
            }

            return entity;
        }

        public void Remove(T entity)
        {
          _dbSet.Remove(entity); // entity state delete olarak işaretlemek için yaptım
        }

        public T Update(T entity)
        {
            _appDbContext.Entry(entity).State=EntityState.Modified; //burda state modife olarak işaretlemek için yaptım

            return entity;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
           return _dbSet.Where(predicate);
        }
    }
}
