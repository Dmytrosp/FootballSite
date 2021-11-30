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
    public class TeamsApiController : ControllerBase
    {



        private readonly DBLibraryContext _context;

        public TeamsApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Teams
        [HttpGet("teams")]
        public async Task<IActionResult> GetTeams()
        {
            
            return Ok(await _context.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var team = await _context.Teams
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            // return View(team);
            return Ok(team);
        }





        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        
        public async Task<IActionResult> Create(Team team)
        {
            if ((DateTime.Now.Year - team.CoachDateOfBirth.Year) < 18 || (DateTime.Now.Year - team.CoachDateOfBirth.Year) > 120)
            {
                return BadRequest("Неправильна дата");
            }

            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }







        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
       
        public async Task<IActionResult> Edit(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if ((DateTime.Now.Year - team.CoachDateOfBirth.Year) < 18 || (DateTime.Now.Year - team.CoachDateOfBirth.Year) > 120)
            {
                return BadRequest("Неправильна дата");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
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

        private bool TeamExists(int teamId)
        {
            return _context.Teams.Any(e => e.TeamId == teamId);
        }







        // POST: Teams/Delete/5
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.TeamId == id);
            if (team == null)
            {
                return NotFound();
            }

            return Ok();
        }






    }
}
