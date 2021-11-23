using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Semester
    {
        public Semester()
        {
            Scores = new HashSet<Score>();
        }
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
