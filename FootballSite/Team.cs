using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
            TeamMatchFirstTeams = new HashSet<TeamMatch>();
            TeamMatchSecondTeams = new HashSet<TeamMatch>();
        }

        public int TeamId { get; set; }



        [Display(Name = "Країна")]

        public int? CountryId { get; set; }



        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Ім'я тренера")]
        public string CoachFirstName { get; set; }



        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Прізвище тренера")]
        public string CoachLastName { get; set; }



        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Дата народження тренера")]
        public DateTime CoachDateOfBirth { get; set; }



        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Біографія тренера")]
        public string CoachBiography { get; set; }




        [Display(Name = "Країна")]
        public virtual Country Country { get; set; }




        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<TeamMatch> TeamMatchFirstTeams { get; set; }
        public virtual ICollection<TeamMatch> TeamMatchSecondTeams { get; set; }
    }
}
