using LMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.IRepo
{
    public interface IModuleRepo
    {

        Task<IEnumerable<Module>> GetAllModules();
        Task<Module> GetModule(string title);
        Task<Module> GetModuleById(int id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T t);

        void Remove(Module module);

        bool IsExists(int id);
    }
}
