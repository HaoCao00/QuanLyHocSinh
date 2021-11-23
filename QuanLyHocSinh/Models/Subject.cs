using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Subject
    {
        public Subject()
        {
            Scores = new HashSet<Score>();
            ScheduleDetails = new HashSet<ScheduleDetail>();
            Teachers = new HashSet<Teacher>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        
        public  virtual ICollection<Score> Scores { get; set; }
        public  virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; }
    }
}
