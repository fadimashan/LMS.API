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
        Task<Module> GetModule(int? Id);
        Task<bool> SaveAsync();
        Task AddAsync<T>(T t);
    }
}
