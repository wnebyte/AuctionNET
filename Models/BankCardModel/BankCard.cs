using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AuctionCore.Models.BankCardModel
{
    public class BankCard
    {
        [Required]
        public BankCardCompany BankCardCompany { get; set; }

        [Required, Display(Name = "Card Number"), 
            DataType(DataType.CreditCard)]
        public int CardNumber { get; set; }

        [Required, Display(Name = "CVC")]
        public int CVC { get; set; }

        [Required, Display(Name = "Name of Card Holder")]
        public string NameOfHolder { get; set; }
    }
}
