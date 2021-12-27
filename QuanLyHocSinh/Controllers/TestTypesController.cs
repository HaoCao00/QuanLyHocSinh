using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypesController : BaseController
    {
        private readonly ITestTypeRepository _testTypeRepository;

        public TestTypesController(ITestTypeRepository testTypeRepository)
        {
            _testTypeRepository = testTypeRepository;
        }

        // GET: api/TestTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestType>>> GettestTypes()
        {
            return await _testTypeRepository.GetAllAsync();
        }

        // GET: api/TestTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestType>> GetTestType(int id)
        {
            var testType = await _testTypeRepository.GetByIdAsync(id);

            if (testType == null)
            {
                return NotFound();
            }

            return testType;
        }

        // PUT: api/TestTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestType(int id, TestType testType)
        {
            if (id != testType.Id)
            {
                return BadRequest();
            }

            try
            {
                await _testTypeRepository.UpdateAsync(testType);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TestTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestType>> PostTestType(TestType testType)
        {
            await _testTypeRepository.AddAsync(testType);

            return CreatedAtAction("GetTestType", new { id = testType.Id }, testType);
        }

        // DELETE: api/TestTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestType(int id)
        {
            await _testTypeRepository.DeleteAsync(id);

            return NoContent();
        } 
    }
}
