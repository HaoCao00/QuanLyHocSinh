using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Models;

namespace QuanLyHocSinh.Services.Interface
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<List<Comment>> GetByNewsFeed(int newsFeedId);
    }
}
