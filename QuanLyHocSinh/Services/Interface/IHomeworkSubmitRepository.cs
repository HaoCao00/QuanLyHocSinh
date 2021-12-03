﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyHocSinh.Models;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IHomeworkSubmitRepository : IBaseRepository<HomeworkSubmit>
    {
        Task<List<HomeworkSubmit>> GetByHomework(int homeworkId);
    }
}