using LMS.Core.IRepo;
using LMS.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Data.Repos
{
    public class WorkUnit : IWorkUnit
    {
        private readonly LMSAPIContext db;

        public ICourseRepo CourseRepo { get; private set; }

        public IModuleRepo ModuleRepo { get; private set; }
        public WorkUnit(LMSAPIContext db)
        {
            this.db = db;
            CourseRepo = new CourseRepo(db);
            ModuleRepo = new ModuleRepo(db);
        }

        public async Task CompleteAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
