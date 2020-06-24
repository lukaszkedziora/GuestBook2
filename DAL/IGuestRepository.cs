using System;
using System.Collections.Generic;
using System.Text;
using GuestBook2.Pages;

namespace GuestBook2.DAL {
    internal interface IGuestRepository {
        List<Record> GetAllRecord();
        bool AddGuest (Record Record);
        // List<Record> GetFewGuest (string amount);
        // Orders GetSingleGuest(int RecordId);
        // bool DeleteGuest(Record Record);
        // bool UpdateGuest(Record Record);
    }
}