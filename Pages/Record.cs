using System.Data.Common;
using System.Data;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GuestBook2.Pages
{
    public class Record
    {
        public string Email {get; set;}
        public string Name {get; set;}
        public string Message {get; set;}
        public DateTime Date {get; set;}
    }
}