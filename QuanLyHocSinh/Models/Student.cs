using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace QuanLyHocSinh.Models
{
    public class Student
    {
        public Student()
        {
            Comments = new HashSet<Comment>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Boolean Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string NameParent { get; set; }
        public string PhoneNumberParent { get; set; }
        public string CMND { get; set; }
        public string PlaceOfIssue { get; set; }
        public DateTime DateOfIssue { get; set; }
        public int ClassId { get; set; }
        public virtual Class ClassNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
