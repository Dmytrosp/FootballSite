using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FootballSite
{
    public partial class Transfer
    {


        public int TransferId { get; set; }


        [Display(Name = "Продавець")]

        public int SellerId { get; set; }



        [Display(Name = "Покупець")]
        public int BuyerId { get; set; }


        [Display(Name = "Гравець")]
        public int? PlayerId { get; set; }




        [Display(Name = "Ціна гравця")]
        public decimal? CostOfPlayer { get; set; }


        [Display(Name = "Дата проведення трансферу")]
        public DateTime? Date { get; set; }


        [Display(Name = "Покупець")]
        public virtual Club Buyer { get; set; }

        [Display(Name = "Гравець")]
        public virtual Player Player { get; set; }

        [Display(Name = "Продавець")]
        public virtual Club Seller { get; set; }
    }
}
