using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class Club
    {
        public Club()
        {
            ClubMatchFirstClubs = new HashSet<ClubMatch>();
            ClubMatchSecondClubs = new HashSet<ClubMatch>();
            Players = new HashSet<Player>();
            TransferBuyers = new HashSet<Transfer>();
            TransferSellers = new HashSet<Transfer>();
        }

        public int ClubId { get; set; }


        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name= "Назва клубу")]
        public string ClubName { get; set; }


        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Ім'я тренера")]
        public string CoachFirstName { get; set; }


        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Прізвище тренера")]
        public string CoachLastName { get; set; }


        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Дата народження")]
        public DateTime? CoachDateOfBirth { get; set; }


        //[Required(ErrorMessage = "Поле не повинно будти порожнім")]
        //[Display(Name = "Назва стадіону")]
        //public string StadiumName { get; set; }


        //[Required(ErrorMessage = "Поле не повинно будти порожнім")]
        //[Display(Name = "Місткість стадіону")]
        //public int? StadiumCapacity { get; set; }

        [Required(ErrorMessage = "Поле не повинно будти порожнім")]
        [Display(Name = "Біографія тренера")]
        public string CoachBiography { get; set; }

        public virtual ICollection<ClubMatch> ClubMatchFirstClubs { get; set; }
        public virtual ICollection<ClubMatch> ClubMatchSecondClubs { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Transfer> TransferBuyers { get; set; }
        public virtual ICollection<Transfer> TransferSellers { get; set; }
    }
}
