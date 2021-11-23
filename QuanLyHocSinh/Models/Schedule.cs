using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public DateTime CreateAt { get; set; }
        public int ClassId { get; set; }
        public virtual Class ClassNavigation { get; set; }
        public virtual ICollection<ScheduleDetail> ScheduleDetails { get; set; }
        public Schedule()
        {
            ScheduleDetails = new HashSet<ScheduleDetail>();
        }
    }
}
