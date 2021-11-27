using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class NewsFeedRepository : BaseRepository<NewsFeed>, INewsFeedRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public NewsFeedRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public override async Task<List<NewsFeed>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.NewsFeeds.OrderByDescending(x => x.CreateAt)
                .ToListAsync();
        }
    }
}
