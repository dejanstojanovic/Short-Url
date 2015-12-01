using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Logic.Exceptions
{
    public class InvalidUrlException : Exception
    {
        public InvalidUrlException(Exception innerException) : base(Resources.ExceptionMessages.InvalidUrlException, innerException)
        {

        }
    }
}
