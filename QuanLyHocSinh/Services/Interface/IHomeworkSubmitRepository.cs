using System.Collections.Generic;
using System.Threading.Tasks;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinh.Services.Interface
{
    public interface IHomeworkSubmitRepository : IBaseRepository<HomeworkSubmit>
    {
        Task<List<HomeworkSubmit>> GetByHomework(int homeworkId);
    }
}
