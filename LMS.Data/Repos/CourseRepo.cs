using AutoMapper.QueryableExtensions;
using LMS.API.Controllers;
using LMS.Core.Entities;
using LMS.Core.Helper;
using LMS.Core.IRepo;
using LMS.Data.Data;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Repos
{
    public class CourseRepo : ICourseRepo
    {
        private readonly LMSAPIContext db;

        public CourseRepo(LMSAPIContext db)
        {
            this.db = db;
        }

        public async Task AddAsync<T>(T t)
        {
            await db.AddAsync(t);
        }

        public async Task<PagedResult<Course>> GetAllAsync(PagingParameters paging)
        {

            IQueryable<Course> courses;
            if (paging.Action == "title")
            {
                courses = paging.IncludeModules ? db.Courses.Include(m => m.Modules).OrderBy(e => e.Title) :
                          db.Courses.OrderBy(e => e.Title);

            }
            else if (paging.Action == "date")
            {
                courses = paging.IncludeModules ? db.Courses.Include(m => m.Modules).OrderBy(e => e.StartDate) :
                           db.Courses.OrderBy(e => e.StartDate);
            }
            else if (paging.Action != null)
            {
                courses = paging.IncludeModules ? db.Courses.Include(m => m.Modules).Where(c => c.Title.StartsWith(paging.Action)) :
                           db.Courses.Where(c => c.Title.StartsWith(paging.Action));
            }
            else
            {
                courses = paging.IncludeModules ? db.Courses.Include(m => m.Modules) :
                                  db.Courses;
            }

            return await PagedResult<Course>.Get(courses, paging.PageNumber, paging.PageSize);

        }



        public async Task<Course> GetCourse(int? Id)
        {
            var oneCourse = await db.Courses.FindAsync(Id);
            return oneCourse;

        }

        public void Remove(Course course)
        {
            db.Courses.Remove(course);
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }


        public bool IsExists(int id)
        {
            var exists = db.Courses.Any(e => e.Id == id);
            return exists;
        }

        public bool IsTitleExists(string title)
        {
            var exists = db.Courses.Any(e => e.Title == title);
            return exists;
        }

     

      
    }
}

