using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Dto
{
    public class CourseDto
    {
        [Required]
        [StringLength(10)]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndTime => StartDate.AddMonths(3);
        public ICollection<ModuleDto> Modules { get; set; }
    }
}
