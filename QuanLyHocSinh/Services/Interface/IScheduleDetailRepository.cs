using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IScheduleDetailRepository:IBaseRepository<ScheduleDetail>
    {
        Task<List<ScheduleDetail>> GetScheduleByClassId(int classId);
        public Task<List<ScheduleDetail>> GetScheduleByDate(string studyDate);
    }
}
