using QuanLyHocSinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
        Task<List<Student>> GetStudentByClassId(int classId);
    }
}
