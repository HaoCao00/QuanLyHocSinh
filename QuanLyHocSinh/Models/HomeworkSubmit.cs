using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Models;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinhClient.Models
{
    public class HomeworkSubmit
    {
        public Guid StudentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public  int HomeworkId { get; set; }
        public  string Content { get; set; }
        public string ImgSources { get; set; }
        public virtual Student StudentNavigation { get; set; }
        public virtual Homework HomeworkNavigation { get; set; } 
    }
}
