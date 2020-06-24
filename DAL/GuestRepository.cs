using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using GuestBook2.Pages;

namespace GuestBook2.DAL
{
    class GuestRespository: IGuestRepository
    {
        private readonly MySqlConnection _db = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        // bool IGuestRepository.DeleteOrder(int productId)
        // {
        //     int rowsAffected = this._db.Execute($"DELETE FROM Products WHERE product_id = @product_id", new { product_id = productId });

        //     if (rowsAffected > 0)
        //     {
        //         return true;
        //     }
        //     return false;

        // }
        List<Record> IGuestRepository.GetAllRecord()
        {
            return this._db.Query<Record>("SELECT Date, Name, Email, Message FROM Records ").ToList();
        }

        // Guest IGuestRepository.GetSingleOrder(int productId)
        // {
        //     return _db.Query<Guest>($"SELECT product_id, category, manufacturer, color, size, price, quantity, description, product_index FROM Products WHERE product_id = @product_id", new { product_id = productId }).SingleOrDefault();
        // }

        // List<Guest> IGuestRepository.GetFewGuest(string amount)
        // {
        //     return this._db.Query<Guest>($"SELECT product_id, category, manufacturer, color, size, price, quantity, description, product_index FROM Products LIMIT {amount}").ToList();
        // }

        bool IGuestRepository.AddGuest(Record record)
        {
            int rowsAffected = this._db.Execute("INSERT INTO Records (Date, Name, Email, Message) VALUES (@Date, @Name, @Email, @Message)", new {Date = record.Date, Name = record.Name, Email = record.Email, Message = record.Message});

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        // bool IGuestRepository.UpdateOrder(Guest ourOrder)
        // {
        //   int rowsAffected = this._db.Execute("UPDATE Guest SET Status = @status WHERE OrderID = " + ourOrder.OrderId, ourOrder);

            
           
            //  if (rowsAffected > 0)
            //  {
            //      return true;
            //  }
           // return false;
       // }

        // Guest IGuestRepository.GetOrderId(int customerId)
        // {
        //     return _db.Query<Guest>($"SELECT OrderID FROM Guest WHERE CustomerID = @customer_id", new { customer_id = customerId }).SingleOrDefault();
        // }
    }
}
