using QuanLyHocSinh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
        Task<List<Student>> GetStudentByClassId(int classId);
        Task AddStudentRange(List<Student> students);
    }
}
