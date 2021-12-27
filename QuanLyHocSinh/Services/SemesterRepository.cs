using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class SemesterRepository:BaseRepository<Semester>, ISemesterRepository
    {
        private readonly IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public SemesterRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        } 
    }
}
