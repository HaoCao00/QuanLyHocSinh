using Microsoft.EntityFrameworkCore;
//using QuanLyHocSinh.Data;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class ScoreRepository : BaseRepository<Score>, IScoreRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public ScoreRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<Score>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Scores
                .Include(x => x.SemesterNavigation)
                .Include(x => x.TestTypeNavigation)
                .Include(x => x.SubjectNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Score>> GetScoresByStudentAndSemester(Guid studentId, int semesterId)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Scores
                .Include(x => x.SemesterNavigation)
                .Include(x => x.TestTypeNavigation)
                .Include(x => x.SubjectNavigation)
                .Where(x => x.StudentId == studentId && x.SemesterId == semesterId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateScoreByStudentId(Guid studentId, int semesterId, int subjectId, double diem15, double diem60, double diemhk, double diemMieng)
        {
            var context = _contextFactory.CreateDbContext();
            var _diem15p = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 2);
            _diem15p.Point = diem15;
            var _diem60p = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 3);
            _diem60p.Point = diem60;
            var _diemHK = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 4);
            _diemHK.Point = diemhk;
            var _diemMieng = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 5);
            _diemMieng.Point = diemMieng;
            context.Scores.UpdateRange(new[] { _diem15p, _diem60p, _diemHK, _diemMieng });
        }

        public async Task UpdateScore(List<Score> scores)
        {
            var context = _contextFactory.CreateDbContext();
            context.Scores.UpdateRange(scores);
            await context.SaveChangesAsync();
        }

        public async Task InitScores(Guid studentId, int SemesterId)
        {
            var context = _contextFactory.CreateDbContext();
            List<Score> list = new List<Score>();
            var testType = context.testTypes.ToList();
            var subject = context.Subjects.ToList();
            foreach (var sb in subject)
            {
                foreach (var tt in testType)
                {
                    var point = context.Scores.FirstOrDefault(x =>
                        x.StudentId == studentId && x.SemesterId == SemesterId && x.TestTypeId == tt.Id &&
                        sb.Id == x.SubjectId);
                    if (point == null)
                    {
                        Score score = new Score()
                        {
                            StudentId = studentId,
                            SemesterId = SemesterId,
                            TestTypeId = tt.Id,
                            SubjectId = sb.Id,
                            Point = -1
                        };
                        list.Add(score);
                    }
                }
            }

            context.Scores.AddRange(list);
            await context.SaveChangesAsync();
        }

        public async Task<List<Score>> GetAllScoreBySemesterId(int semesterId)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Scores
                .Include(x=>x.SubjectNavigation)
                .Include(x=>x.SemesterNavigation)
                .Include(x=>x.TestTypeNavigation)
                .Where(x => x.Point != -1 && x.SemesterId == semesterId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
