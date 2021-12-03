using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinh.Services
{
    public class HomeworkRepository : BaseRepository<Homework>, IHomeworkRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public HomeworkRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public override async Task<List<Homework>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Homework.Include(x=>x.TeacherNavigation)
                .ThenInclude(y=>y.SubjectNavigation)
                .OrderByDescending(x => x.CreateAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
