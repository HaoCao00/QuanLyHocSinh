using Microsoft.EntityFrameworkCore;
//using QuanLyHocSinh.Data;
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
    }
}
