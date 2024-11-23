using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Shared;

namespace DL.Entites
{
    public class building : BaseDomain
    {
        [Required]
        [MinLength(3)]
        public String BuildingName {get; set;}
        [Required]
        [MinLength(3)]
        public String City { get; set;}
        [Range(0, int.MaxValue)]
        public int? NumberOfBedrrooms { get; set;}

        public decimal? RentalRate { get; set;}

    }
}
