using System;
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
    public class ScoresController : BaseController
    {
        private readonly IScoreRepository _scoreRepository;

        public ScoresController(IScoreRepository scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores()
        {
            return await _scoreRepository.GetAllAsync();
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            var score = await _scoreRepository.GetByIdAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        [HttpGet("UpdateScore/{studentId}_{semesterId}_{subjectId}_{diem15P}_{diem60P}_{diemHk}_{diemMieng}")]
        public async Task<ActionResult> UpdateScore(Guid studentId,int semesterId,int subjectId, double diem15P, double diem60P, double diemHk, double diemMieng)
        {
            //if (studenID ==null|| semesterId == null || subjectId == null || diem15p == null || diem60p == null || diemHK == null || diemMieng)
            //{
            //    return NotFound();
            //}
            await _scoreRepository.UpdateScoreByStudentId(studentId, semesterId, subjectId, diem15P, diem60P, diemHk,
                diemMieng);

            return Ok("OK");
        }

        [HttpGet("ScoresByStudentAndSemester/{studentId}/{semesterId}")]
        public async Task<ActionResult<List<Score>>> GetScore(Guid studentId, int semesterId)
        {
            var scores = await _scoreRepository.GetScoresByStudentAndSemester(studentId, semesterId);

            if (scores == null)
            {
                return NotFound();
            }

            return scores;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.SemesterId)
            {
                return BadRequest();
            } 

            try
            {
                await _scoreRepository.UpdateAsync(score);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("UpdateScores")]
        public async Task<IActionResult> UpdateScores(List<Score> scores)
        {
            if (scores == null)
            {
                return BadRequest();
            }

            try
            {
                await _scoreRepository.UpdateScore(scores);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        { 
            try
            {
                await _scoreRepository.AddAsync(score);
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetScore", new { id = score.SemesterId }, score);
        }
        [HttpGet("InitScore/{studentId}/{semesterId}")]
        public async Task<ActionResult<Score>> InitScore(Guid studentId,int semesterId)
        {
            try
            {
                await _scoreRepository.InitScores(studentId, semesterId);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            return NoContent();
        }

        // DELETE: api/Scores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            await _scoreRepository.DeleteAsync(id);

            return NoContent();
        } 
    }
}
