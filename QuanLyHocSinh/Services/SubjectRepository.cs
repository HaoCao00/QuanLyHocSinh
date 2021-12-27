using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class SubjectRepository:BaseRepository<Subject>, ISubjectRepository
    {
        private readonly IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public SubjectRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
       
    }
}
