using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.BuildingDTO;
using DL.Entites;

namespace BL.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<building, CreatBuildingDto>().ReverseMap();
 

        }
    }
}
