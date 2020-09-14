using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GuestBook3.Models;
using Microsoft.AspNetCore.Authorization;

namespace GuestBook3.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IGuestRepository _set;
        private Record _record;
        private List<Record> RecordsList { get; set; }

        public HomeController(ILogger<HomeController> logger, IGuestRepository set, Record record)
        {
            _logger = logger;
            _set = set;
            _record = record;
        }

        [HttpPost]
        public IActionResult Index(Record _record)
        {
            if (ModelState.IsValid)
            {
                AddRecord(_record);
                return RedirectToAction("Index");
            }
            return Index();
        }

        public IActionResult Index()
        {
            ReadFromBase();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private void ReadFromBase()
        {
            RecordsList = _set.GetAllRecord();
            RecordsList.Reverse();
            ViewData["list"] = RecordsList;
        }

        public void AddRecord(Record record)
        {
            record.Date = DateTime.Now;
            _set.AddGuest(record);
            Index();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
