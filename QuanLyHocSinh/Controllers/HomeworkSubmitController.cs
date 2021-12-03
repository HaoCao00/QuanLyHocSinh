using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services;
using QuanLyHocSinh.Services.Interface;
using QuanLyHocSinhClient.Models;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkSubmitController : BaseController
    {
        private readonly IHomeworkSubmitRepository _homeworkSubmitRepository;

        public HomeworkSubmitController(IHomeworkSubmitRepository homeworkSubmitRepository)
        {
            _homeworkSubmitRepository = homeworkSubmitRepository;
        }

        // GET: api/HomeworkSubmits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeworkSubmit>>> GetHomeworkSubmits()
        {
            return await _homeworkSubmitRepository.GetAllAsync();
        }

        // GET: api/HomeworkSubmits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkSubmit>> GetHomeworkSubmit(int id)
        {
            var HomeworkSubmit = await _homeworkSubmitRepository.GetByIdAsync(id);

            if (HomeworkSubmit == null)
            {
                return NotFound();
            }

            return HomeworkSubmit;
        }

        [HttpGet("ByHomeworkId/{homeworkId}")]
        public async Task<ActionResult<List<HomeworkSubmit>>> GetByNewsFeedId(int homeworkId)
        {
            var HomeworkSubmits = await _homeworkSubmitRepository.GetByHomework(homeworkId);

            if (HomeworkSubmits == null)
            {
                return NotFound();
            }

            return HomeworkSubmits;
        }

        // PUT: api/HomeworkSubmits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomeworkSubmit(int id, HomeworkSubmit homeworkSubmit)
        {
            if (id != homeworkSubmit.HomeworkId)
            {
                return BadRequest();
            }

            try
            {
                await _homeworkSubmitRepository.UpdateAsync(homeworkSubmit);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/HomeworkSubmits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HomeworkSubmit>> PostHomeworkSubmit(HomeworkSubmit homeworkSubmit)
        {
            await _homeworkSubmitRepository.AddAsync(homeworkSubmit);

            return CreatedAtAction("GetHomeworkSubmit", new { id = homeworkSubmit.HomeworkId }, homeworkSubmit);
        }

        // DELETE: api/HomeworkSubmits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHomeworkSubmit(int id)
        {
            await _homeworkSubmitRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
