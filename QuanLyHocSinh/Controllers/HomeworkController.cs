using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : BaseController
    {
        private readonly IHomeworkRepository _homeworkRepository;

        public HomeworkController(IHomeworkRepository homeworkRepository)
        {
            _homeworkRepository = homeworkRepository;
        }

        // GET: api/Homeworks
        [HttpGet]
        public async Task<ActionResult<List<Homework>>> GetHomeworks()
        {
            return await _homeworkRepository.GetAllAsync();
        }

        // GET: api/Homeworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> GetHomework(int id)
        {
            var Homework = await _homeworkRepository.GetByIdAsync(id);

            if (Homework == null)
            {
                return NotFound();
            }

            return Homework;
        }

        // PUT: api/Homeworks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomework(int id, Homework homework)
        {
            if (id != homework.Id)
            {
                return BadRequest();
            }

            try
            {
                await _homeworkRepository.UpdateAsync(homework);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Homeworks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Homework>> PostHomework(Homework homework)
        {
            await _homeworkRepository.AddAsync(homework);

            return CreatedAtAction("GetHomework", new { id = homework.Id }, homework);
        }

        // DELETE: api/Homeworks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomework(int id)
        {
            await _homeworkRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
