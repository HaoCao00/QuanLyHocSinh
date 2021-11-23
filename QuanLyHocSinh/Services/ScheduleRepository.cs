using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Services.Interface;
namespace QuanLyHocSinh.Services
{
    public class ScheduleRepository:BaseRepository<Schedule>, IScheduleRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public ScheduleRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<Schedule>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Schedules 
                .Include(x => x.ClassNavigation)
                .AsNoTracking()
                .ToListAsync();
        }
         
    }
}
