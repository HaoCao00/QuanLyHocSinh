using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IScoreRepository:IBaseRepository<Score>
    {
        Task<List<Score>> GetScoresByStudentAndSemester(Guid studentId, int semesterId);
    }
}
