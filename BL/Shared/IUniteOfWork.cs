using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Repositories;
using DL.Shared;


namespace BL.Shared
{
    public interface IUniteOfWork :IAsyncDisposable
    {
      IGenericRepository<T> Repository<T> () where T : BaseDomain;
      BuildingRepository buildingRepository { get; }
        Task<int> CompleteAsync();
    }
}
