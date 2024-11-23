using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Repositories;
using DL.Data;
using DL.Shared;


namespace BL.Shared
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly KRSDBContext _dbcontext;
        private Hashtable _Repositries;

        public BuildingRepository buildingRepository => new BuildingRepository(_dbcontext);

        public UniteOfWork(KRSDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            _Repositries = new Hashtable();
        }
        public IGenericRepository<T> Repository<T>() where T : BaseDomain
        {
            var key = typeof(T).Name;
            if (!_Repositries.ContainsKey(key))
            {
                var repository = new GenericRepository<T>(_dbcontext);
                _Repositries.Add(key, repository);
            }
            return _Repositries[key] as IGenericRepository<T>; 
        }
        public async Task<int> CompleteAsync()
                    => await _dbcontext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
                => await _dbcontext.DisposeAsync();


    }
}
