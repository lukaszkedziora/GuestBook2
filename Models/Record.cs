using System;
using System.ComponentModel.DataAnnotations;

namespace GuestBook3.Models
{
    public class Record
    {   
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, StringLength(1000, MinimumLength = 1)]
        public string Message { get; set; }
        public DateTime Date { get; set; }

    }



}
