using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.Models;
using QuanLyHocSinh.Services.Interface;

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFeedController : BaseController
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
                await _newsFeedRepository.GetAllAsync();
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
            var newsFeed = await _newsFeedRepository.GetByIdAsync(id);

            if (newsFeed == null)
            {
                return NotFound();
            }

            return newsFeed;
        }

        // PUT: api/NewsFeeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsFeed(int id, NewsFeed newsFeed)
        {
            if (id != newsFeed.Id)
            {
                return BadRequest();
            }

            try
            {
                await _newsFeedRepository.UpdateAsync(newsFeed);
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
        public async Task<ActionResult<NewsFeed>> PostNewsFeed(NewsFeed newsFeed)
        {
            await _newsFeedRepository.AddAsync(newsFeed);

            return CreatedAtAction("GetNewsFeed", new { id = newsFeed.Id }, newsFeed);
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
