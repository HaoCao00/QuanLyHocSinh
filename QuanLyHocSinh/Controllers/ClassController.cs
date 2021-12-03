using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ClasssController : BaseController
    {
        private readonly IClassRepository _classRepository;

        public ClasssController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        // GET: api/Classs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasss()
        {
            return await _classRepository.GetAllAsync();
        }

        // GET: api/Classs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var Class = await _classRepository.GetByIdAsync(id);

            if (Class == null)
            {
                return NotFound();
            }

            return Class;
        }

        // PUT: api/Classs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class Class)
        {
            if (id != Class.Id)
            {
                return BadRequest();
            }

            try
            {
                await _classRepository.UpdateAsync(Class);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Classs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class Class)
        {
            await _classRepository.AddAsync(Class);

            return CreatedAtAction("GetClass", new { id = Class.Id }, Class);
        }

        // DELETE: api/Classs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            await _classRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
