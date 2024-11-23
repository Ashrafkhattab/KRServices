using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DL.Data;
using DL.Shared;
using Microsoft.EntityFrameworkCore;

namespace BL.Shared
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDomain
    {
      
        private readonly KRSDBContext _dbContext;
        private DbSet<T> _Set;

        public GenericRepository(KRSDBContext dbContext )
        {
           
            _dbContext = dbContext;
            _Set= _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
           var result =  _dbContext.Add(entity);
            
        }

        public void  Delete(int id)
        {
          var item =  GetById(id);
            item.IsDeleted = true;
            
        }

        public virtual IQueryable<T> GetAll()
        {
            return  _Set.Where(t => t.IsDeleted == false).AsNoTracking();
        }

        public virtual T GetById(int id)
        {
           var entity =  _Set.Find(id);
            if(entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

        public virtual IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _Set.Where(where).AsNoTracking();
        }
        public   void Update(T entity)
        {
           var result =   _Set.Update(entity);
            
        }

        public  void savechanges()
        {
             _dbContext.SaveChanges();
        }
    }
}
