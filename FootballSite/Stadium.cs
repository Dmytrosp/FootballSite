using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballSite
{
    public class Stadium
    {
        public int StadiumId { get; set; }
        public string StadiumName { get; set; }
        public string StadiumDescription { get; set; }
        public string ImageLink { get; set; }
        public double CoordinateX { get; set; }
        public double CoordinateY { get; set; }
        public double TicketPrice { get; set; }
        public int StadiumCapacity { get; set; }
    }
}
