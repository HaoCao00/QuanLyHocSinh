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
    }
}
