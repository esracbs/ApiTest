using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using apiTest.Data;
using apiTest.Models;

namespace apiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _context;
        public IssueController(IssueDbContext context)
        {
            _context = context;
        }
        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _context.Issues.ToList();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }
        [HttpPost("create")]
        public IActionResult Create(Issue issue)
        {
            _context.Issues.Add(issue);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();
            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, Issue issue)
        {
            var issueToDelete = _context.Issues.Find(id);
            if (issueToDelete == null) return NotFound();
            _context.Issues.Remove(issueToDelete);
            _context.SaveChangesAsync();
            return Ok("Başarıyla silindi...");
        }
    }
}
