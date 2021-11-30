using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballSite.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly DBLibraryContext _context;

        public StadiumController(DBLibraryContext context)
        {
            _context = context;
        }
        
        [HttpGet("stadiums")]
        public IActionResult GetStadiums()
        {
            var stadiums = _context.Stadiums.ToList();

            return Ok(stadiums);
        }

        [HttpGet("stadium")]
        public IActionResult GetStadium(int id)
        {
            var stadium = _context.Stadiums.Find(id);

            if (stadium == null) return NotFound();

            return Ok(stadium);
        }

        [HttpDelete("delete-stadium")]
        public IActionResult DeleteStadium(int id)
        {
            var stadium = _context.Stadiums.Find(id);

            if(stadium == null) return NotFound(); 

            return NoContent();
        }

        [HttpPut("update-stadium")]
        public IActionResult UpdateStadium(int id, string name, string description)
        {
            var stadium = _context.Stadiums.Find(id);

            if (stadium == null) return NotFound();

            stadium.StadiumName = name;
            stadium.StadiumDescription = description;

            _context.SaveChanges();

            return Ok(stadium);
        }

    }
}
