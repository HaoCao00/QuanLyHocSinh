using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Services
{
    public class LessonRepository:BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
        } 
    }
}
