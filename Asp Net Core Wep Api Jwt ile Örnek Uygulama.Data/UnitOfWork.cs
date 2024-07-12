using Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Data
{
    public class UnitOfWork : IUnitofWork
    {
        /// <summary>
        /// Veritabanına kaydetmek için dbcontext çapırıldı.İlgili methodlarda dbcontext'in savechange çapırıldı
        /// </summary>
        private readonly DbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void Commit()
        {
           _appDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
          await  _appDbContext.SaveChangesAsync();
        }
    }
}
