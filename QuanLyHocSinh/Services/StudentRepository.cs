using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public class StudentRepository: BaseRepository<Student>, IStudentRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public StudentRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<Student>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Students.Include(x=>x.ClassNavigation).ToListAsync();
        }
    }
}
