using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class Player
    {
        public Player()
        {
            Transfers = new HashSet<Transfer>();
        }

        public int PlayerId { get; set; }



        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }


        [Display(Name = "Прізвище")]
        public string LastName { get; set; }



        public int? TeamId { get; set; }



        public int? ClubId { get; set; }



        [Display(Name = "Дата народження")]
        public DateTime? DateOfBirth { get; set; }


        [Display(Name = "Країна")]
        public int? CountryId { get; set; }



        [Display(Name = "Біографія")]
        public string Biography { get; set; }



        [Display(Name = "Клуб гравця")]
        public virtual Club Club { get; set; }


        [Display(Name = "Країна")]
        public virtual Country Country { get; set; }


        [Display(Name = "Національна збірна")]
        public virtual Team Team { get; set; }
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
