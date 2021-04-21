using LMS.API.Controllers;
using LMS.Core.Entities;
using LMS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.IRepo
{
    public interface ICourseRepo
    {

        Task<Course> GetCourse(int? Id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T course);
        void Remove(Course course);

        bool IsExists(int id);
        bool IsTitleExists(string title);

        Task<PagedResult<Course>> GetAllAsync(PagingParameters paging);
    }
}
