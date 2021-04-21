using LMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Controllers
{
    public class PagedResult<T> : PagedResultBase
    {
        public IEnumerable<T> Data { get; set; }

      
        public PagedResult(IEnumerable<T> data, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Data = data;
        }

        public static async Task<PagedResult<T>> Get(IQueryable<T> items, int pageNumber, int pageSize)
        {
            var count = items.Count();
            var result = await items.Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize).ToListAsync();

            return new PagedResult<T>(result, count, pageNumber, pageSize);

        }
    }
}
