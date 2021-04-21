
using LMS.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApi.PagingExtensions
{
    public static class LinkExtensions
    {
        public static IEnumerable<string> GetPageingLinks<T>(this PagedResult<T> paging, IUrlHelper url, string actionName)
        {
           
            string nextPage = paging.HasNextPage ? url.Link(actionName, new { pageNumber = paging.CurrentPage + 1, pageSize = paging.PageSize }) : null;

            string previousPage = paging.HasPreviousPage ? url.Link(actionName, new { pageNumber = paging.CurrentPage - 1, pageSize = paging.PageSize }) : null;

            return new List<string> { nextPage, previousPage };
        }
    }
}
