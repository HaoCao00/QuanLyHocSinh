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
    public class SchedulesController : BaseController
    {
        private readonly IScheduleRepository _ScheduleRepository;

        public SchedulesController(IScheduleRepository ScheduleRepository)
        {
            _ScheduleRepository = ScheduleRepository;
        }

        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetSchedules()
        {
            return await _ScheduleRepository.GetAllAsync();
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> GetSchedule(int id)
        {
            var Schedule = await _ScheduleRepository.GetByIdAsync(id);

            if (Schedule == null)
            {
                return NotFound();
            }

            return Schedule;
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule Schedule)
        {
            if (id != Schedule.Id)
            {
                return BadRequest();
            }

            try
            {
                await _ScheduleRepository.UpdateAsync(Schedule);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule Schedule)
        {
            await _ScheduleRepository.AddAsync(Schedule);

            return CreatedAtAction("GetSchedule", new { id = Schedule.Id }, Schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await _ScheduleRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
