using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagment.API.CustomExceptions
{
    public class NoUsersFoundException : Exception
    {
        public NoUsersFoundException(string message) : base(message)
        {
        }
    }
}
