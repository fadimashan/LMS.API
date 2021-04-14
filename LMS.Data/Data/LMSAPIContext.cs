using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Data
{
    public class LMSAPIContext : DbContext
    {

        public LMSAPIContext (DbContextOptions<LMSAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Module> Module { get; set; }
    }
}
