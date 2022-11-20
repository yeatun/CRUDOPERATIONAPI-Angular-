using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactList.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Invalid Request")
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
