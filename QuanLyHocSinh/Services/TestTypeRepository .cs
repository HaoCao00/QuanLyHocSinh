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
    public class TestTypeRepository:BaseRepository<TestType>, ITestTypeRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public TestTypeRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
    }
}
