using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class Country
    {
        public Country()
        {
            Players = new HashSet<Player>();
            Teams = new HashSet<Team>();
        }

        [Display(Name = "Країна")]
        public int CountryId { get; set; }

        [Display(Name = "Країна")]
        public string CountryName { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
