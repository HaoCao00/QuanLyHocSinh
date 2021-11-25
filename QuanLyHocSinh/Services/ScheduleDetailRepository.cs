using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Services.Interface;
namespace QuanLyHocSinh.Services
{
    public class ScheduleDetailRepository:BaseRepository<ScheduleDetail>, IScheduleDetailRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public ScheduleDetailRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<ScheduleDetail>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.ScheduleDetails
                .Include(x => x.LessonNavigation)
                .Include(x=>x.ScheduleNavigation)
                .Include(x=>x.SubjectNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ScheduleDetail>> GetScheduleByClassId(int classId)
        {
            var context = _contextFactory.CreateDbContext();
            var schedule = context.Schedules.OrderByDescending(x => x.CreateAt).FirstOrDefault(x => x.ClassId == classId);
            return await context.ScheduleDetails
                .Include(x => x.LessonNavigation)
                .Include(x => x.ScheduleNavigation)
                .Include(x => x.SubjectNavigation)
                .Where(x => x.ScheduleId == schedule.Id)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<ScheduleDetail>> GetScheduleByDate(string studyDate)
        {
            var context = _contextFactory.CreateDbContext();
            List<ScheduleDetail> scheduleDetails = new List<ScheduleDetail>();
            var lessionIds = context.Lessons.Where(x=>x.TimeStart.Equals(studyDate)).ToList();
            foreach (var item in lessionIds)
            {
                var rs =await  context.ScheduleDetails
                    .Include(x => x.LessonNavigation)
                    .Include(x => x.ScheduleNavigation)
                    .Include(x => x.SubjectNavigation)
                    .Where(x => item.Id == x.LessonId)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                if (rs != null)
                {
                    scheduleDetails.Add(rs);
                }
            }

            return scheduleDetails;
        }
    }
}
