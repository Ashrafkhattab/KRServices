using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SharedDTO
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } =1;
        private int _PageSize =500;

        public int PageSize
        {
            get { return _PageSize;  }
            set { _PageSize  = value; }
        }
        public string? OrderBy { get; set; }
    }
}
