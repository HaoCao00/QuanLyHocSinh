using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Boolean Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject SubjectNavigation { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public Teacher()
        {
            Classes = new HashSet<Class>();
        }
    }
}
