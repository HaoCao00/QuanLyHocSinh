using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class TestType
    {
        public TestType()
        {
            Scores = new HashSet<Score>();
        }
        public int Id { get; set; }
        public string TestName { get; set; }
        public double ScoreFactor { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
