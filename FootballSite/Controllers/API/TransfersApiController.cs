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
    public class TransfersApiController : ControllerBase
    {




        private readonly DBLibraryContext _context;

        public TransfersApiController(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Transfers
        [HttpGet("transfers")]
        public async Task<IActionResult> Index()
        {
            var dBLibraryContext = _context.Transfers.Include(t => t.Buyer).Include(t => t.Player).Include(t => t.Seller);
            return Ok(await dBLibraryContext.ToListAsync());
        }

        // GET: Transfers/Details/5
        [HttpGet("details")]
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

            return Ok(transfer);
        }





        // POST: Transfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("TransferId,SellerId,BuyerId,PlayerId,CostOfPlayer,Date")] Transfer transfer)
        {

            if (transfer.CostOfPlayer < 0)
            {
                
                return BadRequest("Ціна не може бути від'ємною");
            }

            if (_context.Players.Where(x => x.ClubId == transfer.SellerId).Any(x => x.PlayerId == transfer.PlayerId))
            {
                if (transfer.SellerId != transfer.BuyerId)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(transfer);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
                else
                {
                    return BadRequest("Команди співпадають");
                }

            }
            else
            {
                return BadRequest("Гравець не з команди продавця");
            }
            
            return BadRequest();
        }







        // POST: Transfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit")]
    
        public async Task<IActionResult> Edit(int id, Transfer transfer)
        {
            if (id != transfer.TransferId)
            {
                return NotFound();
            }

            if (transfer.CostOfPlayer < 0)
            {
                return BadRequest("Ціна не може бути від'ємною");
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
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Команди співпадають");
                    }
                }
            }
            else
            {
                return BadRequest("Гравець не з команди продавця");
            }
            return BadRequest();
        }

        private bool TransferExists(int transferId)
        {
            return _context.Transfers.Any(e => e.TransferId == transferId);
        }






        // GET: Transfers/Delete/5
        [HttpPost("delete")]
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

            return Ok();
        }



    }
}
