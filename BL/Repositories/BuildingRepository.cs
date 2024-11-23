using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Data;
using BL.DTOs.BuildingDTO;
using BL.Helpers;
using BL.Shared;
using DL.Entites;

namespace BL.Repositories
{
    public interface IBuildingRepository
    {
        IQueryable<building> GetAllBuildings(BuildingPrameters pram);
    }
    public class BuildingRepository : GenericRepository<building>, IBuildingRepository
    {
        public BuildingRepository(KRSDBContext ctx):base(ctx)
        {
                
        }

        public /*PagedList<*/IQueryable< building> GetAllBuildings(BuildingPrameters pram)
        {
            IQueryable<building> buildings = GetAll();

            SearchByPrameters(ref buildings, pram);
            //return PagedList<building>.ToPagedList(buildings , pram.PageNumber, pram.PageSize);
            return buildings;
        }


        private void SearchByPrameters(ref IQueryable<building> source , BuildingPrameters pram)
        {
            if (!source.Any())
            {

                if(!string.IsNullOrEmpty(pram.Name))
                source = source.Where(o => o.BuildingName.ToLower().Contains(pram.Name.ToLower()));

                if (!string.IsNullOrEmpty(pram.City)) 
                    source = source.Where(o => o.City.ToLower().Contains(pram.City.ToLower()));

                if (pram.NumberOfBeds != null || pram.NumberOfBeds !=0)
                    source = source.Where(o => o.NumberOfBedrrooms == pram.NumberOfBeds);

            }
        }

    }
}
