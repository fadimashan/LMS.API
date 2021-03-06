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

        public async Task<Module> GetModule(string title)
        {
            var module = await db.Modules.Where(m => m.Title == title).FirstOrDefaultAsync();
            return module;
        }

        public async Task<Module> GetModuleById(int id)
        {
            var module = await db.Modules.FindAsync(id);
            return module;
        }

        public async Task<bool> SaveAsync()
        {
            return (await db.SaveChangesAsync()) >= 0;
        }


        public void Remove(Module module)
        {
            db.Modules.Remove(module);
        }


        public bool IsExists(int id)
        {
            var exists = db.Modules.Any(e => e.Id == id);
            return exists;
        }
    }
}
