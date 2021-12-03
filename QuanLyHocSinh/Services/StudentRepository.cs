using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public StudentRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<Student>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Students
                .Include(x => x.ClassNavigation)
                .ToListAsync();
        }

        public Task<List<Student>> GetStudentByClassId(int classId)
        {
            var context = _contextFactory.CreateDbContext();
            return context.Students
                .Include(x => x.ClassNavigation)
                .Where(x => x.ClassId == classId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddStudentRange(List<Student> students)
        {
            var context = _contextFactory.CreateDbContext();
            foreach (var item in students)
            {
                var user = context.Accounts.Add(new Account()
                { UserName = item.Email.Split("@")[0], Password = item.Email.Split("@")[0], Role = "student" });
                item.Id = user.Entity.Id;
                context.Students.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
