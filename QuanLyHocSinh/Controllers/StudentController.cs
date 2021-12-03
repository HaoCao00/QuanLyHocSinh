using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : BaseController
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILoginRepository _loginRepository;


        public StudentsController(IStudentRepository studentRepository, ILoginRepository loginRepository)
        {
            _studentRepository = studentRepository;
            _loginRepository = loginRepository;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _studentRepository.GetAllAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var Student = await _studentRepository.GetByIdAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Student;
        }
         
        [HttpGet("studentByClassId/{classId}")]
        public async Task<ActionResult<List<Student>>> GetStudentByClassId(int classId)
        {
            var Students = await _studentRepository.GetStudentByClassId(classId);

            if (Students == null)
            {
                return NotFound();
            }

            return Students;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student Student)
        {
            if (id != Student.Id)
            {
                return BadRequest();
            }

            try
            {
                await _studentRepository.UpdateAsync(Student);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student, string username, string password)
        {
            var user = await _loginRepository.AddAsync(new Account()
                {UserName = username, Password = password, Role = "student"});
            student.Id = user.Id;
            await _studentRepository.AddAsync(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpPost]
        [Route("addRange")]
        public async Task<ActionResult<List<Student>>> AddRangeStudent(List<Student> students)
        {
            await _studentRepository.AddStudentRange(students);

            return students;
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
