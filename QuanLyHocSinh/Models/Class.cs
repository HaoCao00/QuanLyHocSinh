using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SchoolYear { get; set; }
        public Guid TeacherId { get; set; }
        public virtual Teacher TeacherNavigation { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public Class()
        {
            Students = new HashSet<Student>();
            Schedules = new HashSet<Schedule>();
        }

    }
}
