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
    [Authorize]
    public class CrudController: Controller
    {
        private IGuestRepository _set;
        private List<Record> RecordsList { get; set; }
        public CrudController(IGuestRepository set)
        {
            _set = set;
        }

        public IActionResult Admin()
        {
            ReadFromBase();
            return View();
        }

        private void ReadFromBase()
        {
            RecordsList = _set.GetAllRecord();
            RecordsList.Reverse();
            ViewData["list"] = RecordsList;
        }

        public IActionResult DeleteRecord(int id)
        {
            _set.DeleteRecord(id);
            return RedirectToAction("Admin");
        }

        public IActionResult EditRecord(int id)
        {
            return View(_set.GetSingleRecord(id));
        }

        [HttpPost]
        public IActionResult EditRecord(Record record)
        {
            _set.UpdateRecord(record);
            return RedirectToAction("Admin");
        }


    }

}