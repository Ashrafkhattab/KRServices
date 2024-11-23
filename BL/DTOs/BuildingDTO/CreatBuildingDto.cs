using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.SharedDTO;

namespace BL.DTOs.BuildingDTO
{
    public class CreatBuildingDto : BaseDomainDto
    {
        [Required]
        [MinLength(3)]
        public String BuildingName { get; set; }
        [Required]
        [MinLength(3)]
        public String City { get; set; }

        [Range(0, int.MaxValue)]
        public int? NumberOfBedrrooms { get; set; }

        public decimal? RentalRate { get; set; }
    }
}
