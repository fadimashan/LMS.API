using Bogus;
using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Data.Data
{
    public class SeedData
    {
        private static Faker faker;
        private static List<Course> courses;
        private static List<Module> modules;
        public static async Task InitiAsync(IServiceProvider services)
        {
            using var db = new LMSAPIContext(services.GetRequiredService<DbContextOptions<LMSAPIContext>>());

            faker = new Faker();
            courses = GetCourses(5);

            if (!await db.Courses.AnyAsync())
            {
                foreach (var item in courses)
                {
                    await db.Courses.AddAsync(item);
                }

                await db.SaveChangesAsync();
            }
            courses = await db.Courses.ToListAsync();

            modules = GetModules(10);
            if (!await db.Modules.AnyAsync())
            {

                foreach (var item in modules)
                {
                    await db.Modules.AddAsync(item);
                }
            }
            await db.SaveChangesAsync();

        }

        private static List<Course> GetCourses(int num)
        {
            courses = new List<Course>();
            for (int i = 0; i < num; i++)
            {
                var course = new Course()
                {
                    Title = faker.Company.CatchPhrase(),
                    StartDate = faker.Date.Between(new DateTime(2022, 01, 01), new DateTime(2025, 01, 01))
                };


                courses.Add(course);
            }

            return courses;
        }


        private static List<Module> GetModules(int num)
        {

            var listOfId = new List<int>();
            for (int i = 0; i < (courses.Count); i++)
            {
                listOfId.Add(courses[i].Id);
                listOfId.Add(courses[i].Id);

            };
            modules = new List<Module>();
            for (int i = 0; i < num; i++)
            {
               // int courseid = (i != num - 1) ? faker.Random.Int(listOfId[0], listOfId.Count) : listOfId[0];
                int courseid = (i != num - 1) ? faker.Random.ListItem(listOfId) : listOfId[0];
               // var newcourseid = courseid > courses.Count ? faker.Random.Int(0, courses.Count) : courseid;

                var module = new Module()
                {
                    Title = faker.Company.CatchPhrase(),
                    StartDate = faker.Date.Between(new DateTime(2022, 01, 01), new DateTime(2025, 01, 01)),
                    CourseId = courseid
                };
                var toRemove = listOfId.IndexOf(courseid);


                if (listOfId.Count > toRemove && toRemove != -1) { listOfId.RemoveAt(toRemove); } else { listOfId.RemoveAt(0); };

                modules.Add(module);
            }

            return modules;
        }
    }
}
