using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuctionCore.Models.BankCardModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AuctionCore.Models.UserModel
{
    public class User
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        [Required, Display(Name = "* Username"), 
            MinLength(3, ErrorMessage = "Usernames are required to be at least 3 characters long.")]
        public string Username { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "password")]
        [Required, Display(Name = "* Password"), 
            DataType(DataType.Password), 
            MinLength(6, ErrorMessage = "Passwords are required to be at least 6 characters long.")]
        public string Password { get; set; }

        [BsonIgnore]
        [Required, Display(Name = "* Confirm Password"),  
            DataType(DataType.Password), 
            MinLength(6, ErrorMessage = "Passwords are required to be at least 6 characters long.")]
        [Compare("Password"), NotMapped]
        public string PasswordConfirm { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "emailAddress")]
        [Required, Display(Name = "* Email"),  
            DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [BsonIgnore]
        [Required, Display(Name = "* Confirm Email"),  
            DataType(DataType.EmailAddress)]
        [Compare("EmailAddress"), NotMapped]
        public string EmailAddressConfirm { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement(elementName: "phoneNumber")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [BsonRequired]
        [BsonElement(elementName: "address")]
        public Address Address { get; set; }

        [BsonIgnore]
        public List<BankCard> BankCards { get; set; }
    }
}
