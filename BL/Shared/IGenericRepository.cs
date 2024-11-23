using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Shared;

namespace BL.Shared
{
    public interface IGenericRepository<T> where T : BaseDomain
    {
       IQueryable<T> GetAll();  
       T GetById(int id);
        void Add(T entity);
        void Update (T entity);
        void  Delete (int id);
        void savechanges();
     

    }
}
