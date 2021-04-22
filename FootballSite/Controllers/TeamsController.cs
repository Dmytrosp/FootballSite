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
    public class TeamsController : Controller
    {
        private readonly DBLibraryContext _context;

        public TeamsController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.Teams.Include(t => t.Country);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
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

            // return View(team);
            return RedirectToAction("Index", "PlayersForTeams", new { id = team.TeamId});
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            var existedTeams = _context.Teams.Include(t => t.Country).Select(t => t.Country);
            ViewData["CountryId"] = new SelectList(_context.Countries.ToList().Except(existedTeams), "CountryId", "CountryName");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryId,CoachFirstName,CoachLastName,CoachDateOfBirth,CoachBiography")] Team team)
        {
            if ((DateTime.Now.Year - team.CoachDateOfBirth.Year) < 18 || (DateTime.Now.Year - team.CoachDateOfBirth.Year) > 120)
            {
                var existedTeams1 = _context.Teams.Include(t => t.Country).Select(t => t.Country);
                ViewData["CountryId"] = new SelectList(_context.Countries.ToList().Except(existedTeams1), "CountryId", "CountryName");
                ModelState.AddModelError("CoachDateOfBirth", "Неправильна дата");
                return View(team);
            }

            if (ModelState.IsValid)
            {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

            var existedTeams = _context.Teams.Include(t => t.Country).Select(t => t.Country);
            ViewData["CountryId"] = new SelectList(_context.Countries.ToList().Except(existedTeams), "CountryId", "CountryName");
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryName", team.CountryId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,CountryId,CoachFirstName,CoachLastName,CoachDateOfBirth,CoachBiography")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if ((DateTime.Now.Year - team.CoachDateOfBirth.Year) < 18 || (DateTime.Now.Year - team.CoachDateOfBirth.Year) > 120)
            {
                ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", team.CountryId);
                ModelState.AddModelError("CoachDateOfBirth", "Неправильна дата");
                return View(team);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", team.CountryId);
            return View(team);
        }

        // GET: Teams/Delete/5
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

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
