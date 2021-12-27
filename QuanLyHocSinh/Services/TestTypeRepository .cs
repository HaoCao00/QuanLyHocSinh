using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class TestTypeRepository:BaseRepository<TestType>, ITestTypeRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public TestTypeRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        } 
    }
}
