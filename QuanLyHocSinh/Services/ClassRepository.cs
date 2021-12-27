using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class ClassRepository:BaseRepository<Class>, IClassRepository
    {
        private readonly IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public ClassRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public override async Task<List<Class>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Classes
                .Include(x => x.TeacherNavigation)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
