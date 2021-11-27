using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Models;

namespace QuanLyHocSinh.Services.Interface
{
    public interface ILoginRepository : IBaseRepository<Account>
    {
        Task<Account> GetUser(string email, string password);
        Task<Account> GetByUserName(string userName);
        Task<(Student, Teacher)> GetUserOrTeacher(Guid Id);
    }
}
