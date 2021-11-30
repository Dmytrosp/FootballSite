using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballSite;
using Microsoft.AspNetCore.Mvc;


namespace GalleryApplication.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChartsController : ControllerBase
    {
        private readonly DBLibraryContext _context;

        public ChartsController(DBLibraryContext context)
        {
            _context = context;
        }

        [HttpGet("PlayerCountryData")]
        public IActionResult PlayerCountryData()
        {

            var countries = _context.Countries.Where(x => x.Players.Count() != 0).Select(x => new { countryName = x.CountryName, count = x.Players.Count()}).ToList();
            //_context.Countries.Where(x => x.Players.Count() != 0).ToList();
            List<object> result = new List<object>();

            result.Add(new[] { "Країна", "Кількість" });

            foreach (var country in countries)
            {
                result.Add(new object[] { country.countryName, country.count });
            }

            return new JsonResult(result);
        }

        [HttpGet("StadiumData")]
        public IActionResult StadiumData()
        {
            var stadiums = _context.Stadiums.Select(x => new { stadiumName = x.StadiumName, stadiumCapacity = x.StadiumCapacity });

            List<object> result = new List<object>();

            result.Add(new[] { "Стадіон", "Місткість" });

            foreach (var stadium in stadiums)
            {
                result.Add(new object[] { stadium.stadiumName, stadium.stadiumCapacity });
            }

            return new JsonResult(result);
        }
    }
}