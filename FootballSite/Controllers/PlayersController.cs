using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballSite;

namespace FootballSite.Controllers
{
    public class PlayersController : Controller
    {
        private readonly DBLibraryContext _context;

        public PlayersController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Clubs");

            ViewBag.ClubId = id;
            ViewBag.ClubName = name == null ? _context.Clubs.Find(id).ClubName: name;
            var playersByClub = _context.Players.Where(b => b.ClubId == id).Include(b => b.Club).Include(p => p.Country);

            var dBLibraryContext = _context.Players.Include(p => p.Club).Include(p => p.Country).Include(p => p.Team).ThenInclude(p => p.Country);
            return View(await playersByClub.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.Club)
                .Include(p => p.Country)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create(int clubId)
        {
            ViewBag.ClubId = clubId;
            ViewBag.ClubName = _context.Clubs.Where(c => c.ClubId == clubId).FirstOrDefault().ClubName;


            /*
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
          //  ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            */

            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");

            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int clubId, [Bind("PlayerId,FirstName,LastName,ClubId,DateOfBirth,CountryId,Biography")] Player player)
        {
            player.ClubId = clubId;

            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Players", new { id = clubId, name = _context.Clubs.Where(c=> c.ClubId== clubId).FirstOrDefault().ClubName});
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            //  ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", player.Club.ClubName);
             ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName");
            //  ViewData["TeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", player.TeamId);
            //  return View(player);
            return RedirectToAction("Index", "Players", new { id = player.ClubId});
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", player.CountryId);
           
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FirstName,LastName,DateOfBirth,CountryId,Biography")] Player player)
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
                return RedirectToAction(nameof(Index));
            }
            string messages = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));

            ViewData["ClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName", player.ClubId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", player.CountryId);
            
            return View(player);
        }

        // GET: Players/Delete/5
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

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var player = await _context.Players.FindAsync(id);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
