using System.Collections.Generic;
using GuestBook3.Models;

namespace GuestBook3.Controllers
{
    public interface IGuestRepository
    {
        List<Record> GetAllRecord();
        bool AddGuest(Record Record);
        Record GetSingleRecord(int RecordId);
        bool DeleteRecord(int Id);
        bool UpdateRecord(Record Record);
    }
}
