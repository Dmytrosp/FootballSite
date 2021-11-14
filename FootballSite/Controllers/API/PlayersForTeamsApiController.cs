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
    public class PlayersForTeamsApiController : ControllerBase
    {
        private readonly DBLibraryContext _context;

        public PlayersForTeamsApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Players
        [HttpGet("playersforteam")]
        public async Task<IActionResult> GetPlayersByTeam(int? id, string? name)
        {
            if (id == null) return NotFound();

          //  ViewBag.TeamId = id;
           // ViewBag.TeamName = name == null ? (await _context.Teams.Include(c => c.Country).SingleOrDefaultAsync(x => x.TeamId == id)).Country.CountryName : name;
            var playersByTeam = _context.Players.Where(b => b.TeamId == id).Include(b => b.Team).ThenInclude(c => c.Country).Include(b => b.Country);

            var dBLibraryContext = _context.Players.Include(p => p.Club).Include(p => p.Country).Include(p => p.Team).ThenInclude(p => p.Country);
            return Ok(await playersByTeam.ToListAsync());
        }




        // GET: Players/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Club)
                .Include(p => p.Country)
                .Include(p => p.Team).ThenInclude(c => c.Country)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }





        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        
        public async Task<IActionResult> Create(int teamId, Player player)
        {
            player.TeamId = teamId;

            if ((DateTime.Now.Year - player.DateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - player.DateOfBirth.Value.Year) > 120)
            {
                return BadRequest("Неправильна дата");
            }

            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }





        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
       
        public async Task<IActionResult> Edit(int id, Player player)
        {
            var playerToEdit = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            playerToEdit.FirstName = player.FirstName;
            playerToEdit.LastName = player.LastName;
            playerToEdit.DateOfBirth = player.DateOfBirth;
            playerToEdit.CountryId = player.CountryId;
            playerToEdit.Biography = player.Biography;
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if ((DateTime.Now.Year - player.DateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - player.DateOfBirth.Value.Year) > 120)
            {
               
                return BadRequest("Неправильна дата");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerToEdit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerId))
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

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }




        // GET: Players/Delete/5
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Club)
                .Include(p => p.Country)
                .Include(p => p.Team).ThenInclude(p => p.Country)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return Ok();
        }




    }
}
