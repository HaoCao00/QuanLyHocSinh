﻿using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services
{
    public class ClassRepository:BaseRepository<Class>, IClassRepository
    {
        private IDbContextFactory<QuanLyHocSinhContext> _contextFactory;
        public ClassRepository(IDbContextFactory<QuanLyHocSinhContext> context) : base(context)
        {
            _contextFactory = context;
        }
        public async override Task<List<Class>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Classes.Include(x => x.TeacherNavigation).ToListAsync() ;
        }
    }
}
