using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : BaseController
    {
        private readonly ILessonRepository _LessonRepository;

        public LessonsController(ILessonRepository LessonRepository)
        {
            _LessonRepository = LessonRepository;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            return await _LessonRepository.GetAllAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var Lesson = await _LessonRepository.GetByIdAsync(id);

            if (Lesson == null)
            {
                return NotFound();
            }

            return Lesson;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson Lesson)
        {
            if (id != Lesson.Id)
            {
                return BadRequest();
            }

            try
            {
                await _LessonRepository.UpdateAsync(Lesson);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesson>> PostLesson(Lesson Lesson)
        {
            await _LessonRepository.AddAsync(Lesson);

            return CreatedAtAction("GetLesson", new { id = Lesson.Id }, Lesson);
        }

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _LessonRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
