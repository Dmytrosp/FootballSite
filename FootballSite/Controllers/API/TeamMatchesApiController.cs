using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballSite.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMatchesApiController : ControllerBase
    {



        private readonly DBLibraryContext _context;

        public TeamMatchesApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: TeamMatches
        [HttpGet("teammatches")]
        public async Task<IActionResult> GetTeamMaches()
        {
            return Ok(await _context.TeamMatches.ToListAsync());
        }
        
        // GET: TeamMatches/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMatch = await _context.TeamMatches
                .Include(t => t.FirstTeam)
                .Include(t => t.SecondTeam)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (teamMatch == null)
            {
                return NotFound();
            }

            return Ok(teamMatch);

        }






        // POST: TeamMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        public async Task<IActionResult> Create(TeamMatch teamMatch)
        {
            if (teamMatch.FirstTeamId != teamMatch.SecondTeamId)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(teamMatch);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }






        // POST: TeamMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
      
        public async Task<IActionResult> Edit(int id, TeamMatch teamMatch)
        {
            if (id != teamMatch.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamMatchExists(teamMatch.MatchId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

        private bool TeamMatchExists(int matchId)
        {
            return _context.TeamMatches.Any(e => e.MatchId == matchId);
        }

        // GET: TeamMatches/Delete/5
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMatch = await _context.TeamMatches
                .Include(t => t.FirstTeam)
                .Include(t => t.SecondTeam)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (teamMatch == null)
            {
                return NotFound();
            }

            return Ok();
        }




    }
}
