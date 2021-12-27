using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class ScoreRepository : BaseRepository<Score>, IScoreRepository
    {
        private readonly IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
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

        public async Task UpdateScoreByStudentId(Guid studentId,int semesterId,int subjectId, double diem15, double diem60, double diemhk, double diemMieng)
        {
            var context = _contextFactory.CreateDbContext();
            var diem15P = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 2);
            diem15P.Point = diem15;
            var diem60P = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 3);
            diem60P.Point = diem60;
            var diemHk = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 4);
            diemHk.Point = diemhk;
            var diemM = await context.Scores.FirstOrDefaultAsync(x =>
                x.StudentId == studentId && x.SemesterId == semesterId && x.SubjectId == subjectId &&
                x.TestTypeId == 5);
            diemM.Point = diemMieng;
            context.Scores.UpdateRange(new []{ diem15P , diem60P , diemHk , diemM });
        }

        public async Task UpdateScore(List<Score> scores)
        {
            var context = _contextFactory.CreateDbContext();  
            context.Scores.UpdateRange(scores);
            await context.SaveChangesAsync();
        }

        public async Task InitScores(Guid studentId, int semesterId)
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
                        x.StudentId == studentId && x.SemesterId == semesterId && x.TestTypeId == tt.Id &&
                        sb.Id == x.SubjectId);
                    if (point == null)
                    {
                        Score score = new Score()
                        {
                            StudentId = studentId,
                            SemesterId = semesterId,
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
    }
}
