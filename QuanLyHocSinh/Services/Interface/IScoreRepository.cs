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

        Task UpdateScoreByStudentId(Guid studentId, int semesterId, int subjectId, double diem15, double diem60,
            double diemhk, double diemMieng);

        Task UpdateScore(List<Score> scores);
        Task InitScores(Guid studentId, int SemesterId);
    }
}
