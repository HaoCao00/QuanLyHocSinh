using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class LoginRepository : BaseRepository<Account>, ILoginRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public LoginRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public async Task<Account> GetUser(string username, string password)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Accounts.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        public async Task<Account> GetByUserName(string userName)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Accounts.FirstOrDefaultAsync(x => x.UserName.Equals(userName));
          
        }

        public async Task<(Student, Teacher)> GetUserOrTeacher(Guid Id)
        {
            var context = _contextFactory.CreateDbContext(); 
            var student =await context.Students.FirstOrDefaultAsync(x=>x.Id== Id);
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.Id == Id);

            return (student, teacher);
        }
    }
}
