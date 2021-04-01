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
    public class TransfersController : Controller
    {
        private readonly DBLibraryContext _context;

        public TransfersController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Transfers
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.Transfers.Include(t => t.Buyer).Include(t => t.Player).Include(t => t.Seller);
            return View(await dBLibraryContext.ToListAsync());
        }

        // GET: Transfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.Buyer)
                .Include(t => t.Player)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.TransferId == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public IActionResult Create()
        {
            ViewData["BuyerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["PlayerId"] = new SelectList(_context.Players.Where(x => x.ClubId > 0), "PlayerId", "LastName");
            ViewData["SellerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransferId,SellerId,BuyerId,PlayerId,CostOfPlayer,Date")] Transfer transfer)
        {
            if (_context.Players.Where(x => x.ClubId == transfer.SellerId).Any(x => x.PlayerId == transfer.PlayerId))
            {
                if(transfer.SellerId != transfer.BuyerId)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(transfer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ModelState.AddModelError("BuyerId", "Команди співпадають");
                }
                
            }
            else
            {
                ModelState.AddModelError("PlayerId", "Гравець не з команди продавця");
            }
            ViewData["BuyerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["PlayerId"] = new SelectList(_context.Players.Where(x => x.ClubId > 0), "PlayerId", "LastName");
            ViewData["SellerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View(transfer);
        }

        // GET: Transfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers.SingleOrDefaultAsync(x => x.TransferId == id);
            if (transfer == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["PlayerId"] = new SelectList(_context.Players.Where(x => x.ClubId > 0), "PlayerId", "LastName");
            ViewData["SellerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransferId,SellerId,BuyerId,PlayerId,CostOfPlayer,Date")] Transfer transfer)
        {
            if (id != transfer.TransferId)
            {
                return NotFound();
            }

            if (_context.Players.Where(x => x.ClubId == transfer.SellerId).Any(x => x.PlayerId == transfer.PlayerId))
            {
                if (ModelState.IsValid)
                {
                    if (transfer.SellerId != transfer.BuyerId)
                    {
                        try
                        {
                            _context.Update(transfer);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!TransferExists(transfer.TransferId))
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
                    else
                    {
                        ModelState.AddModelError("BuyerId", "Команди співпадають");
                    }
                }
            } 
            else
            {
                ModelState.AddModelError("PlayerId", "Гравець не з команди продавця");
            }
            ViewData["BuyerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            ViewData["PlayerId"] = new SelectList(_context.Players.Where(x => x.ClubId > 0), "PlayerId", "LastName");
            ViewData["SellerId"] = new SelectList(_context.Clubs, "ClubId", "ClubName");
            return View(transfer);
        }

        // GET: Transfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transfer = await _context.Transfers
                .Include(t => t.Buyer)
                .Include(t => t.Player)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.TransferId == id);
            if (transfer == null)
            {
                return NotFound();
            }

            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transfer = await _context.Transfers.SingleOrDefaultAsync(x => x.TransferId == id);
            _context.Transfers.Remove(transfer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferExists(int id)
        {
            return _context.Transfers.Any(e => e.TransferId == id);
        }
    }
}
