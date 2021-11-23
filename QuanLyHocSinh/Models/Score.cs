using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Score
    { 
        public DateTime CreatedAt { get; set; }
        public Guid StudentId { get; set; }
        public int SemesterId { get; set; } 
        public int TestTypeId { get; set; } 
        public int SubjectId { get; set; }
        public double Point { get; set; }
        public virtual Semester SemesterNavigation { get; set; }
        public virtual TestType TestTypeNavigation { get; set; }
        public virtual Subject SubjectNavigation { get; set; }
    }
}
