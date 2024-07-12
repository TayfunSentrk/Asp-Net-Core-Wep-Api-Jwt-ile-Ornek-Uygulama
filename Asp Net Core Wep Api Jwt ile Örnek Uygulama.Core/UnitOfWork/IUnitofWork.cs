using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp_Net_Core_Wep_Api_Jwt_ile_Örnek_Uygulama.Core.UnitOfWork
{
    public interface IUnitofWork
    {
        /// <summary>
        /// Burda unitofwork kullanmamın sebebi birbiri ie ilişiki olan verilerde bir hata olduğundan diğerinin veritabınına yazılması .Roll Back yapılması
        /// </summary>
       

        Task CommitAsync();
        void Commit();
    }
}
