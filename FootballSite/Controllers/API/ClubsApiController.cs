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
    public class ClubsApiController : ControllerBase
    {

        private readonly DBLibraryContext _context;

        public ClubsApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Clubs
        [HttpGet("clubs")]
        public async Task<IActionResult> GetClubs()
        {

            return Ok(await _context.Clubs.ToListAsync());
        }

        // GET: Clubs/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .FirstOrDefaultAsync(m => m.ClubId == id);
            if (club == null)
            {
                return NotFound();
            }

            return Ok(club);
        }





        // GET: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet("create")]
        public async Task<IActionResult> Create(Club club)
        {

            if ((DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) > 120)
            {


                return BadRequest("Неправильна дата");
            }



                if (ModelState.IsValid)
                {
                    _context.Add(club);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                return BadRequest();
         

        }




        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
       
        public async Task<IActionResult> Edit(int id,  Club club)
        {
            if (id != club.ClubId)
            {
                return NotFound();
            }

            if ((DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) > 120)
            {
                return BadRequest("Неправильна дата");
            }

           

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(club);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.ClubId))
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

        private bool ClubExists(int clubId)
        {
            return _context.Clubs.Any(e => e.ClubId == clubId);
        }


        // POST: Clubs/Delete/5
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var club = await _context.Clubs
                .FirstOrDefaultAsync(m => m.ClubId == id);
            if (club == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
