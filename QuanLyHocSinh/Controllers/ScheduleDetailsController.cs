using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDetailsController : BaseController
    {
        private readonly IScheduleDetailRepository _scheduleDetailRepository;

        public ScheduleDetailsController(IScheduleDetailRepository scheduleDetailRepository)
        {
            _scheduleDetailRepository = scheduleDetailRepository;
        }

        // GET: api/ScheduleDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDetail>>> GetScheduleDetails()
        {
            return await _scheduleDetailRepository.GetAllAsync();
        }

        // GET: api/ScheduleDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDetail>> GetScheduleDetail(int id)
        {
            var scheduleDetail = await _scheduleDetailRepository.GetByIdAsync(id);

            if (scheduleDetail == null)
            {
                return NotFound();
            }

            return scheduleDetail;
        }

        [HttpGet("ScheduleByClassId/{classid}")]
        public async Task<ActionResult<List<ScheduleDetail>>> GetScheduleByClassId(int classid)
        {
            var scheduleDetail = await _scheduleDetailRepository.GetScheduleByClassId(classid);

            if (scheduleDetail == null)
            {
                return NotFound();
            }

            return scheduleDetail;
        }

        [HttpGet("scheduleDetailByDate/{studyDate}")]
        public async Task<ActionResult<List<ScheduleDetail>>> GetScheduleByClassId(string studyDate)
        {
            var scheduleDetail = await _scheduleDetailRepository.GetScheduleByDate(studyDate);

            if (scheduleDetail == null)
            {
                return NotFound();
            }

            return scheduleDetail;
        }

        // PUT: api/ScheduleDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduleDetail(int id, ScheduleDetail scheduleDetail)
        {
            if (id != scheduleDetail.ScheduleId)
            {
                return BadRequest();
            }

            try
            {
                await _scheduleDetailRepository.UpdateAsync(scheduleDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/ScheduleDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScheduleDetail>> PostScheduleDetail(ScheduleDetail scheduleDetail)
        {
            try
            {
                await _scheduleDetailRepository.AddAsync(scheduleDetail);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetScheduleDetail", new { id = scheduleDetail.ScheduleId }, scheduleDetail);

        }

        // DELETE: api/ScheduleDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduleDetail(int id)
        {
            await _scheduleDetailRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
