using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using GuestBook3.Models;

namespace GuestBook3.Controllers.DAL
{
    public class AccessDataRepository : IAccessData
    {

        private readonly MySqlConnection _db = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);


        bool IAccessData.AddAccessData(AccessData accessData)
        {
            int rowsAffected = this._db.Execute("INSERT INTO AccessData (UserName, Password) VALUES (@UserName, @Password)", new { UserName = accessData.UserName, Password = accessData.Password});

            if (rowsAffected > 0)
            {
                return true;
            }
            return false;
        }

        AccessData IAccessData.GetAccessData(AccessData accessData)
        {      
            return _db.Query<AccessData>("SELECT UserName, Password FROM AccessData WHERE UserName = @Username", new {Username = accessData.UserName}).SingleOrDefault();
        }
    }
}

