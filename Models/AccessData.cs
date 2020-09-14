using System.ComponentModel.DataAnnotations;

namespace GuestBook3.Models
{
    public class AccessData
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
  
    }
}