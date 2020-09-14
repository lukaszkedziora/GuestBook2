using System.Collections.Generic;
using GuestBook3.Models;

namespace GuestBook3.Controllers.DAL
{
    public interface IAccessData
    {
        AccessData GetAccessData(AccessData accessData);
        bool AddAccessData(AccessData accessData);
    }
}