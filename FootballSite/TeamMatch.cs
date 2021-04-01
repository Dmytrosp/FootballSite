using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class TeamMatch
    {

        
        public int MatchId { get; set; }

        [Display(Name = "Матч")]
        public string MatchName { get; set; }


        [Display(Name = "Результат матчу")]
        public string MatchResult { get; set; }


        [Display(Name = "Команда 1")]
        public int FirstTeamId { get; set; }


        [Display(Name = "Команда 2")]
        public int SecondTeamId { get; set; }

        [Display(Name = "Дата проведення матчу")]
        public DateTime? MatchDate { get; set; }

        [Display(Name = "Команда 1")]
        public virtual Team FirstTeam { get; set; }
        [Display(Name = "Команда 2")]
        public virtual Team SecondTeam { get; set; }
    }
}
