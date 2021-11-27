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
    public class ScheduleDetailsController : BaseController
    {
        private readonly IScheduleDetailRepository _ScheduleDetailRepository;

        public ScheduleDetailsController(IScheduleDetailRepository ScheduleDetailRepository)
        {
            _ScheduleDetailRepository = ScheduleDetailRepository;
        }

        // GET: api/ScheduleDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDetail>>> GetScheduleDetails()
        {
            return await _ScheduleDetailRepository.GetAllAsync();
        }

        // GET: api/ScheduleDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDetail>> GetScheduleDetail(int id)
        {
            var ScheduleDetail = await _ScheduleDetailRepository.GetByIdAsync(id);

            if (ScheduleDetail == null)
            {
                return NotFound();
            }

            return ScheduleDetail;
        }

        [HttpGet("ScheduleByClassId/{classid}")]
        public async Task<ActionResult<List<ScheduleDetail>>> GetScheduleByClassId(int classid)
        {
            var ScheduleDetail = await _ScheduleDetailRepository.GetScheduleByClassId(classid);

            if (ScheduleDetail == null)
            {
                return NotFound();
            }

            return ScheduleDetail;
        }

        [HttpGet("scheduleDetailByDate/{studyDate}")]
        public async Task<ActionResult<List<ScheduleDetail>>> GetScheduleByClassId(string studyDate)
        {
            var ScheduleDetail = await _ScheduleDetailRepository.GetScheduleByDate(studyDate);

            if (ScheduleDetail == null)
            {
                return NotFound();
            }

            return ScheduleDetail;
        }

        // PUT: api/ScheduleDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScheduleDetail(int id, ScheduleDetail ScheduleDetail)
        {
            if (id != ScheduleDetail.ScheduleId)
            {
                return BadRequest();
            }

            try
            {
                await _ScheduleDetailRepository.UpdateAsync(ScheduleDetail);
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
        public async Task<ActionResult<ScheduleDetail>> PostScheduleDetail(ScheduleDetail ScheduleDetail)
        {
            try
            {
                await _ScheduleDetailRepository.AddAsync(ScheduleDetail);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetScheduleDetail", new { id = ScheduleDetail.ScheduleId }, ScheduleDetail);

        }

        // DELETE: api/ScheduleDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScheduleDetail(int id)
        {
            await _ScheduleDetailRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
