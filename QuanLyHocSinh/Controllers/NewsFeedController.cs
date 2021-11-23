using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedRepository _newsFeedRepository;

        public NewsFeedController(INewsFeedRepository newsFeedRepository)
        {
            _newsFeedRepository = newsFeedRepository;
        }

        // GET: api/NewsFeeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsFeed>>> GetNewsFeeds()
        {
            try
            {
                var kq = await _newsFeedRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return await _newsFeedRepository.GetAllAsync();
        }

        // GET: api/NewsFeeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsFeed>> GetNewsFeed(int id)
        {
            var NewsFeed = await _newsFeedRepository.GetByIdAsync(id);

            if (NewsFeed == null)
            {
                return NotFound();
            }

            return NewsFeed;
        }

        // PUT: api/NewsFeeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsFeed(int id, NewsFeed NewsFeed)
        {
            if (id != NewsFeed.Id)
            {
                return BadRequest();
            }

            try
            {
                await _newsFeedRepository.UpdateAsync(NewsFeed);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/NewsFeeds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NewsFeed>> PostNewsFeed(NewsFeed NewsFeed)
        {
            await _newsFeedRepository.AddAsync(NewsFeed);

            return CreatedAtAction("GetNewsFeed", new { id = NewsFeed.Id }, NewsFeed);
        }

        // DELETE: api/NewsFeeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsFeed(int id)
        {
            await _newsFeedRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
