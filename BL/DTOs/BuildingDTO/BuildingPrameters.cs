using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.SharedDTO;

namespace BL.DTOs.BuildingDTO
{
    public class BuildingPrameters :PaginationParameters
    {
        public string? Name {  get; set; }
        public string? City { get; set; }

        public int? NumberOfBeds { get; set; }
    }
}
