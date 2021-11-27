using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; } 
        public  string Role { get; set; }
    }
}
