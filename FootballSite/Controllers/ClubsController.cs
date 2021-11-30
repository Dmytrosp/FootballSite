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
    public class ClubsController : Controller
    {
        private readonly DBLibraryContext _context;

        public ClubsController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Clubs
        public async Task<IActionResult> Index()
        {
          
            return View(await _context.Clubs.ToListAsync());
        }

        // GET: Clubs/Details/5
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

            return RedirectToAction("Index", "Players", new { id = club.ClubId, name = club.ClubName });
        }
       

        // GET: Clubs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClubId,ClubName,CoachFirstName,CoachLastName,CoachDateOfBirth,StadiumName,StadiumCapacity,CoachBiography")] Club club)
        {

            if ((DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) > 120)
            {
               
                ModelState.AddModelError("CoachDateOfBirth", "Неправильна дата");
                return View(club);
            }

            //if(club.Stadium.StadiumCapacity < 0)
            //{
            //    ModelState.AddModelError("StadiumCapacity", "Місткість не може бути від'ємною");
            //    return View(club);
            //}

            if (ModelState.IsValid)
            {
                _context.Add(club);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClubId,ClubName,CoachFirstName,CoachLastName,CoachDateOfBirth,StadiumName,StadiumCapacity,CoachBiography")] Club club)
        {
            if (id != club.ClubId)
            {
                return NotFound();
            }

            if ((DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) < 18 || (DateTime.Now.Year - club.CoachDateOfBirth.Value.Year) > 120)
            {

                ModelState.AddModelError("CoachDateOfBirth", "Неправильна дата");
                return View(club);
            }

            //if (club.Stadium.StadiumCapacity < 0)
            //{
            //    ModelState.AddModelError("StadiumCapacity", "Місткість не може бути від'ємною");
            //    return View(club);
            //}

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
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // GET: Clubs/Delete/5
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

            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            var transfersToDelete = _context.Transfers.Where(x => x.BuyerId == id || x.SellerId == id);
            _context.Transfers.RemoveRange(transfersToDelete);
            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.ClubId == id);
        }
    }
}
