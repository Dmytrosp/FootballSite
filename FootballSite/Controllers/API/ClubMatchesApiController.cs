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
    public class ClubMatchesApiController : ControllerBase
    {





        private readonly DBLibraryContext _context;

        public ClubMatchesApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: ClubMatches
        [HttpGet("clubmatches")]
        public async Task<IActionResult> GetClubMatches()
        {
            
            return Ok(await _context.ClubMatches.ToListAsync());
        }

        // GET: ClubMatches/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubMatch = await _context.ClubMatches
                .Include(c => c.FirstClub)
                .Include(c => c.SecondClub)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (clubMatch == null)
            {
                return NotFound();
            }

            return Ok(clubMatch);
        }






        // POST: ClubMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
      
        public async Task<IActionResult> Create(ClubMatch clubMatch)
        {
            if (clubMatch.FirstClubId != clubMatch.SecondClubId)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(clubMatch);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }






        // POST: ClubMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
        
        public async Task<IActionResult> Edit(int id,  ClubMatch clubMatch)
        {
            if (id != clubMatch.MatchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubMatchExists(clubMatch.MatchId))
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

        private bool ClubMatchExists(int matchId)
        {
            return _context.ClubMatches.Any(e => e.MatchId == matchId);
        }






        // POST: ClubMatches/Delete/5
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubMatch = await _context.ClubMatches
                .Include(c => c.FirstClub)
                .Include(c => c.SecondClub)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (clubMatch == null)
            {
                return NotFound();
            }

            return Ok();
        }







    }
}
