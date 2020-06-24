using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using GuestBook2.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GuestBook2.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        private IGuestRepository Set = new GuestRespository();
        [BindProperty] 
        public Record Records { get; set; }
        private List<Record> RecordsList { get; set; }

        public IndexModel (ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet () {
            ReadFromBase();
        }

        private void ReadFromBase() {
            RecordsList = Set.GetAllRecord();
            RecordsList.Reverse();
            ViewData["list"] = RecordsList;
        }

        public void AddRecord(){
            if (Records.Name != null & Records.Email != null & Records.Message != null) {
            Records.Date = DateTime.Now;
            Set.AddGuest(Records);
            }
        }

        public  IActionResult OnPost() {
            AddRecord();
            return RedirectToPage("Index");
            
        }

    }
}