using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook3.Models
{
    public class AddAccessData
    {
        [Required, Remote(action: "VerifyUserName", controller: "Account")]
        public string UserName { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        [Required, Compare(nameof(Password), ErrorMessage = "Password don't match, please try again")]
        public string RepeatPassword { get; set; }
    }

}

