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

namespace QuanLyHocSinh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _commentRepository.GetAllAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var Comment = await _commentRepository.GetByIdAsync(id);

            if (Comment == null)
            {
                return NotFound();
            }

            return Comment;
        }

        [HttpGet("ByNewsFeedId/{newsFeedId}")]
        public async Task<ActionResult<List<Comment>>> GetByNewsFeedId(int newsFeedId)
        {
            var comments = await _commentRepository.GetByNewsFeed(newsFeedId);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment Comment)
        {
            if (id != Comment.NewsFeedId)
            {
                return BadRequest();
            }

            try
            {
                await _commentRepository.UpdateAsync(Comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment Comment)
        {
            await _commentRepository.AddAsync(Comment);

            return CreatedAtAction("GetComment", new { id = Comment.NewsFeedId }, Comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
