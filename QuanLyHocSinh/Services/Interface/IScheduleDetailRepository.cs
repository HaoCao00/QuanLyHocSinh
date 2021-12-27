using QuanLyHocSinh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IScheduleDetailRepository:IBaseRepository<ScheduleDetail>
    {
        Task<List<ScheduleDetail>> GetScheduleByClassId(int classId);
        public Task<List<ScheduleDetail>> GetScheduleByDate(string studyDate);
    }
}
