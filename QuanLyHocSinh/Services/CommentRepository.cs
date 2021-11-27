using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public CommentRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public override async Task<List<Comment>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Comments
                .Include(x => x.NewsFeedNavigation)
                .Include(x => x.StudentNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Comment>> GetByNewsFeed(int newsFeedId)
        {
            var context = _contextFactory.CreateDbContext();

            return await context.Comments
                .Include(x => x.StudentNavigation)
                .Where(x=>x.NewsFeedId == newsFeedId)
                .OrderByDescending(x=>x.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
