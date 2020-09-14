using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using MySql.Data;
using GuestBook3.Models;

namespace GuestBook3.Controllers
{
    class GuestRepository : IGuestRepository
    {

        private readonly MySqlConnection _db = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        List<Record> IGuestRepository.GetAllRecord()
        {
            return this._db.Query<Record>("SELECT Id, Date, Name, Email, Message FROM Records ").ToList();
        }

        bool IGuestRepository.AddGuest(Record record)
        {
            int rowsAffected = this._db.Execute("INSERT INTO Records (Date, Name, Email, Message) VALUES (@Date, @Name, @Email, @Message)", new { Date = record.Date, Name = record.Name, Email = record.Email, Message = record.Message });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        bool IGuestRepository.DeleteRecord(int id)
        {
            int rowsAffected = this._db.Execute(@"DELETE FROM Records WHERE Id = @Id", new { Id = id });

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
        Record IGuestRepository.GetSingleRecord(int recordId)
        {
            return _db.Query<Record>("SELECT Name, Email, Message FROM Records WHERE Id = @Id", new { Id = recordId }).SingleOrDefault();
        }

        public bool UpdateRecord(Record record)
        {
            int rowsAffected = this._db.Execute("UPDATE Records SET Name = @Name , Email = @Email, Message = @Message WHERE Id = " + record.Id, record);

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }
    }
}


