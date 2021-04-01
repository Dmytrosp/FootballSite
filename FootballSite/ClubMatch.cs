using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace FootballSite
{
    public partial class ClubMatch
    {
        public int MatchId { get; set; }



        [Display(Name = "Назва матчу")]
        public string MatchName { get; set; }



        [Display(Name = "Результат матчу")]
        public string MatchResult { get; set; }



        [Display(Name = "Команда 1")]
        public int FirstClubId { get; set; }


        [Display(Name = "Команда 2")]
        public int SecondClubId { get; set; }


        [Display(Name = "Дата проведення матчу")]
        public DateTime? MatchDate { get; set; }


        [Display(Name = "Команда 1")]

        public virtual Club FirstClub { get; set; }



        [Display(Name = "Команда 2")]
        public virtual Club SecondClub { get; set; }
    }
}
