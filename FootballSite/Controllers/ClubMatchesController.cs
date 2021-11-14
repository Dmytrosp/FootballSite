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
    public class ClubMatchesController : Controller
    {
        private readonly DBLibraryContext _context;

        public ClubMatchesController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: ClubMatches
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.ClubMatches.Include(c => c.FirstClub).Include(c => c.SecondClub);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: ClubMatches/Details/5
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

            return View(clubMatch);
        }


        // GET: ClubMatches/Create
        public IActionResult Create()
        {
            ViewData["FirstClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["SecondClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View();
        }

        // POST: ClubMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchId,MatchName,MatchResult,FirstClubId,SecondClubId,MatchDate")] ClubMatch clubMatch)
        {
            if (clubMatch.FirstClubId != clubMatch.SecondClubId)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(clubMatch);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["FirstClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["SecondClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View(clubMatch);
        }

        // GET: ClubMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clubMatch = await _context.ClubMatches.SingleOrDefaultAsync(x => x.MatchId == id);
            if (clubMatch == null)
            {
                return NotFound();
            }
            ViewData["FirstClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubMatch.FirstClubId);
            ViewData["SecondClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubMatch.SecondClubId);
            return View(clubMatch);
        }

        // POST: ClubMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchId,MatchName,MatchResult,FirstClubId,SecondClubId,MatchDate")] ClubMatch clubMatch)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubMatch.FirstClubId);
            ViewData["SecondClubId"] = new SelectList(_context.Clubs, "ClubId", "ClubId", clubMatch.SecondClubId);
            return View(clubMatch);
        }

        // GET: ClubMatches/Delete/5
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

            return View(clubMatch);
        }

        // POST: ClubMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clubMatch = await _context.ClubMatches.SingleOrDefaultAsync(x => x.MatchId == id);
            _context.ClubMatches.Remove(clubMatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubMatchExists(int id)
        {
            return _context.ClubMatches.Any(e => e.MatchId == id);
        }
    }
}
