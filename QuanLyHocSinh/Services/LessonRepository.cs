using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class LessonRepository:BaseRepository<Lesson>, ILessonRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public LessonRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        } 
    }
}
