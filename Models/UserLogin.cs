using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AuctionCore.Models
{
    public class UserLogin
    {
        [Required, Display(Name = "Email"), 
            DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required, Display(Name = "Password"),  
            DataType(DataType.Password)]
        public string Password { get; set; }

        public string ToJSON() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
