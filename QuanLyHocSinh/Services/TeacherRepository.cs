﻿using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class TeacherRepository: BaseRepository<Teacher>, ITeacherRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public TeacherRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }

        public override async Task<List<Teacher>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Teachers
                .Include(x => x.SubjectNavigation)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
