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
    public class TeamMatchesController : Controller
    {
        private readonly DBLibraryContext _context;

        public TeamMatchesController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: TeamMatches
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.TeamMatches.Include(t => t.FirstTeam).Include(t => t.SecondTeam);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: TeamMatches/Details/5
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

            return View(teamMatch);
           
        }

        // GET: TeamMatches/Create
        public IActionResult Create()
        {
            var teams = _context.Teams.Include(t => t.Country).Select(x => new { TeamId = x.TeamId, CountryName = x.Country.CountryName });
            ViewData["FirstTeamId"] = new SelectList(teams, "TeamId", "CountryName");
            ViewData["SecondTeamId"] = new SelectList(teams, "TeamId", "CountryName");
            return View();
        }

        // POST: TeamMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchName,MatchResult,FirstTeamId,SecondTeamId,MatchDate")] TeamMatch teamMatch)
        {
            if (teamMatch.FirstTeamId != teamMatch.SecondTeamId)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(teamMatch);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var teams = _context.Teams.Include(t => t.Country).Select(x => new { TeamId = x.TeamId, CountryName = x.Country.CountryName });
            ViewData["FirstTeamId"] = new SelectList(teams, "TeamId", "CountryName");
            ViewData["SecondTeamId"] = new SelectList(teams, "TeamId", "CountryName");
            return View(teamMatch);
        }

        // GET: TeamMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamMatch = await _context.TeamMatches.SingleOrDefaultAsync(x => x.MatchId == id);
            if (teamMatch == null)
            {
                return NotFound();
            }
            ViewData["FirstTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", teamMatch.FirstTeamId);
            ViewData["SecondTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", teamMatch.SecondTeamId);
            return View(teamMatch);
        }

        // POST: TeamMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,MatchName,MatchResult,FirstTeamId,SecondTeamId,MatchDate")] TeamMatch teamMatch)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", teamMatch.FirstTeamId);
            ViewData["SecondTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId", teamMatch.SecondTeamId);
            return View(teamMatch);
        }

        // GET: TeamMatches/Delete/5
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

            return View(teamMatch);
        }

        // POST: TeamMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamMatch = await _context.TeamMatches.SingleOrDefaultAsync(x => x.MatchId == id);
            _context.TeamMatches.Remove(teamMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamMatchExists(int id)
        {
            return _context.TeamMatches.Any(e => e.MatchId == id);
        }
    }
}
