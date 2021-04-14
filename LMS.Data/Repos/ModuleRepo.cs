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
    public class ModuleRepo : IModuleRepo
    {
        private readonly LMSAPIContext db;

        public ModuleRepo(LMSAPIContext db)
        {
            this.db = db;
        }
        public async Task AddAsync<T>(T t)
        {
            await db.AddAsync(t);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            var modules = await db.Modules.ToListAsync();
            return modules;
        }

        public async Task<Module> GetModule(int? Id)
        {
            var module = await db.Modules.FindAsync(Id);
            return module;
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }
    }
}
