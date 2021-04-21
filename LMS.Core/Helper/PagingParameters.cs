using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Helper
{
    public class PagingParameters : FilterParameters
    {
        private int pageSize = 5;

        [Range(1, int.MaxValue)]
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value > 20 ? 20 : value;
            }
        }

        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;



    }
}
