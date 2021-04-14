using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LMS.Data.Data
{
    public class SeedData
    {
        public static async Task InitiAsync(IServiceProvider services)
        {
            using var db = new LMSAPIContext(services.GetRequiredService<DbContextOptions<LMSAPIContext>>());
            if (!await db.Courses.AnyAsync())
            {
                var courseOne = new Course()
                {
                    Title = "Java",
                    StartDate = new DateTime(2022, 01, 01)

                };
                await db.Courses.AddAsync(courseOne);
                await db.SaveChangesAsync();
            }


            if (!await db.Modules.AnyAsync())
            {
                var moduleOne = new Module()
                {
                    Title = "Api",
                    StartDate = new DateTime(2022, 06, 01),
                    CourseId = 1

                };
                await db.Modules.AddAsync(moduleOne);
                await db.SaveChangesAsync();
            }

        }
    }
}
