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
    public class TeachersController : BaseController
    {
        private readonly ITeacherRepository _TeacherRepository;

        private readonly ILoginRepository _loginRepository;

        public TeachersController(ITeacherRepository TeacherRepository, ILoginRepository loginRepository)
        {
            _TeacherRepository = TeacherRepository;
            _loginRepository = loginRepository;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _TeacherRepository.GetAllAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var Teacher = await _TeacherRepository.GetByIdAsync(id);

            if (Teacher == null)
            {
                return NotFound();
            }

            return Teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(Guid id, Teacher Teacher)
        {
            if (id != Teacher.Id)
            {
                return BadRequest();
            }

            try
            {
                await _TeacherRepository.UpdateAsync(Teacher);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher, string username, string password)
        {
            var account = await _loginRepository.AddAsync(new Account()
                {UserName = username, Password = password, Role = "teacher"});
            teacher.Id = account.Id;
            await _TeacherRepository.AddAsync(teacher);

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _TeacherRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
