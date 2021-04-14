using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.IRepo
{
    public interface IWorkUnit
    {

        ICourseRepo CourseRepo { get; }
        IModuleRepo ModuleRepo { get; }

        Task CompleteAsync();

    }
}
