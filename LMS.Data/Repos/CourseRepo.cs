using LMS.Core.Entities;
using LMS.Core.IRepo;
using LMS.Data.Data;
using Microsoft.EntityFrameworkCore;
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
            //await db.Courses.AddAsync(course);
            await db.AddAsync(t);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var courses = await db.Courses.Include(m=> m.Modules).ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourse(int? Id)
        {
            var oneCourse = await db.Courses.FindAsync(Id);
            return oneCourse;

        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
    }
}
