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
    public class HomeworkSubmitRepository : BaseRepository<HomeworkSubmit>, IHomeworkSubmitRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public HomeworkSubmitRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public override async Task<List<HomeworkSubmit>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();

            return await context.HomeworkSubmit
                .Include(x => x.HomeworkNavigation)
                .Include(x => x.StudentNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<HomeworkSubmit>> GetByHomework(int homeworkId)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.HomeworkSubmit
                .Include(x => x.StudentNavigation)
                .Where(x=>x.HomeworkId == homeworkId)
                .OrderByDescending(x=>x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
